namespace Quitaye.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthClientController : ControllerBase
    {
        const string Callback = "https://quitaye.mahalfial.com/signin-google";
        private readonly IGenericRepositoryWrapper<Client> _userRepository;
        private readonly IGenericRepositoryWrapper<ExternalLogin> _externalLoginRepository;
        private readonly IConfigSettings _settings;
        private readonly IFacebook _facebook;
        private readonly IGenericRepositoryWrapper<RefreshToken> _refreshTokenRepository;
        public AuthClientController(IGenericRepositoryWrapper<Client> userRepository,
            IGenericRepositoryWrapper<RefreshToken> refreshTokenRepository,
            IGenericRepositoryWrapper<ExternalLogin> externalLoginRepository,
            IFacebook facebook,
            IConfigSettings settings)
        {
            _userRepository = userRepository;
            _settings = settings;
            _facebook = facebook;
            _externalLoginRepository = externalLoginRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Secrets>> LogIn([FromBody] LogInModel logIn)
        {
            try
            {
                if (logIn == null)
                    BadRequest("Invalid process");


                if (!string.IsNullOrWhiteSpace(logIn.Token))
                {
                    var resultUser = await RefreshToken(logIn);
                    if (string.IsNullOrWhiteSpace(resultUser.Value.Username.ToString())
                        && (!string.IsNullOrWhiteSpace(logIn.Username) 
                        && !string.IsNullOrWhiteSpace(logIn.Password)))
                    {
                        var result = await _userRepository.Item.GetBy(x => x.Username == logIn.Username 
                        && x.EntrepriseId == logIn.EntrepriseId 
                        && x.Password == _settings.PaswordEncryption(logIn.Password + _settings.Key));
                        if (result.Count() != 0)
                        {
                            var token = await GenerateAccessToken(result.First().Id);
                            Secrets user = new Secrets();
                            user.Username = result.First().Username;
                            user.ProfilePic = result.First().PhotoUrl;
                            user.Prenom = result.First().Prenom;
                            user.Email = result.First().Email;
                            user.TokenExpiry = result.First().DateOfCreation;
                            user.Nom = result.First().Nom;
                            user.Token = token.Token;
                            user.Success = true;
                            return Ok(user);
                        }
                        else return NotFound("Utilisateur invalid");
                    }
                    else
                    {
                        return Ok(resultUser);
                    }
                }
                else
                {
                    if ((!string.IsNullOrWhiteSpace(logIn.Username) && !string.IsNullOrWhiteSpace(logIn.Password)))
                    {
                        var result = await _userRepository.Item.GetBy(x => x.Username == logIn.Username 
                        && x.EntrepriseId == logIn.EntrepriseId 
                        && x.Password == _settings.PaswordEncryption(logIn.Password + _settings.Key));
                        if (result.Count() != 0)
                        {
                            var token = await GenerateAccessToken(result.First().Id);
                            Secrets user = new Secrets();
                            user.Username = result.First().Username;
                            user.ProfilePic = result.First().PhotoUrl;
                            user.Prenom = result.First().Prenom;
                            user.TokenExpiry = result.First().DateOfCreation;
                            user.Email = result.First().Email;
                            user.Nom = result.First().Nom;
                            user.Token = token.Token;

                            return Ok(user);
                        }
                        else return NotFound("Utilisateur invalid");
                    }
                    else return BadRequest("Invalid Process");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<Secrets>> RefreshToken([FromBody] LogInModel refreshRequest)
        {
            var user = await GetUserFromAccessToken(refreshRequest.Token);

            if (user != null && await ValidateRefreshToken(refreshRequest.Token))
            {
                var result = await GenerateAccessToken(user.Id);
                Secrets userWithToken = new Secrets();
                userWithToken.Username = user.Username;
                userWithToken.Token = result.Token;
                userWithToken.Prenom = result.Prenom;
                userWithToken.Nom = result.Nom;
                userWithToken.TokenExpiry = result.DateOfExpiry;
                userWithToken.Email = user.Email;
                userWithToken.ProfilePic = user.PhotoUrl;
                userWithToken.Success = true;
                return userWithToken;
            }
            return null;
        }

        const string callbackScheme = "mahalfial";

        [HttpGet("ExternalLogin/{scheme}")]
        public async Task ExternalLogIn([FromRoute] string scheme)
        {
            //NOTE: see https://docs.microsoft.com/en-us/xamarin/essentials/web-authenticator?tabs=android
            var auth = await Request.HttpContext.AuthenticateAsync(scheme);

            if (!auth.Succeeded || auth?.Principal == null || !auth.Principal.Identities.Any(id => id.IsAuthenticated)
                || string.IsNullOrEmpty(auth.Properties.GetTokenValue("access_token")))
            {
                // Not authenticated, challenge
                await Request.HttpContext.ChallengeAsync(scheme);
                //return null;
            }
            else
            {
                var claims = auth.Principal.Identities.FirstOrDefault()?.Claims;

                var email = string.Empty;
                email = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
                var givenName = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.GivenName)?.Value;
                var surName = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Surname)?.Value;
                var nameIdentifier = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                string picture = string.Empty;

                if (scheme == "Facebook")
                {
                    picture = await _facebook.GetFacebookProfilePicURL(auth.Properties.GetTokenValue("access_token"));
                }
                else if (scheme == "Google")
                    picture = claims?.FirstOrDefault(c => c.Type == "picture")?.Value;

                var item = await _userRepository.Item.GetBy(x => x.Email == email);
                Client user = null;
                if (item.Count() != 0)
                {
                    user = item.First();
                } else
                {
                    user = await _userRepository.Item.AddAsync(new Client()
                    {
                        Email = email,
                        Prenom = givenName,
                        Nom = surName,
                        PhotoUrl = picture,
                        Id = Guid.NewGuid(),
                        DateOfCreation = DateTime.Now,
                    });

                    await _userRepository.SaveAsync();
                }

                var authToken = await GenerateAccessToken(user.Id);
                await _externalLoginRepository.Item.AddAsync(new ExternalLogin()
                {
                    UserId = user.Id,
                    Token = authToken.Token,
                    DateOfExpiry = authToken.DateOfExpiry,
                    DateOfLogin = DateTime.Now,
                    Provider = scheme,
                    Id = Guid.NewGuid()
                });

                await _externalLoginRepository.SaveAsync();

                var qs = new Dictionary<string, string>
                {
                    { "access_token", authToken.Token },
                    {"dateofexpiry", authToken.DateOfExpiry.ToString() },
                    { "refresh_token",  string.Empty },
                    { "email", email },
                    { "firstName", givenName },
                    { "picture", picture },
                    { "secondName", surName },
                };

                //Build the result url
                var url = Callback + "://#" + string.Join(
                    "&",
                    qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

                //Redirect to final url
                Request.HttpContext.Response.Redirect(url);
            }
        }

        [Authorize]
        [HttpGet("signout")]
        public async Task Signout()
        {
            await Request.HttpContext.SignOutAsync();
            //await _signInManager.SignOutAsync();
        }

        // GET: api/Users
        [HttpPost("GetUserByAccessToken")]
        public async Task<ActionResult<LogInModel>> GetUserByAccessToken([FromBody] string accessToken)
        {
            LogInModel user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                return user;
            }
            return null;
        }

        [HttpPost("TokenCheck")]
        public async Task<ActionResult<RefreshToken>> RefreshTokenExpiryCheck([FromBody] LogInModel accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_settings.Key);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken.Token, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var Id = principle.FindFirst(ClaimTypes.Name)?.Value;
                    var user = await _refreshTokenRepository.Item.GetBy(x => x.Token == accessToken.Token && x.DateOfExpiry >= DateTime.Now);
                    if (user.Count() != 0)
                    {
                        return user.First();
                    }
                    else return null;
                }
                else return null;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("expir"))
                {
                    return null;
                }
                else return null;
            }
        }

        //[Authorize]
        [HttpGet("ExternalUserByAccessToken/")]
        public async Task<ActionResult<UserProfile>> GetExternalUserByAccessToken([FromBody] string accessToken)
        {
            LogInModel user = await GetUserFromAccessToken(accessToken);

            if (user != null)
            {
                var u = await _userRepository.Item.GetBy(x => x.Id == user.Id);
                if (u.Count() != 0)
                {
                    return new UserProfile()
                    {
                        UserId = u.First().Id,
                        Url = u.First().PhotoUrl,
                        Email = u.First().Email,
                        
                        Nom = u.First().Nom,
                        Prenom = u.First().Prenom,
                        Token = user.Token,
                    };
                }
            }
            return null;
        }

        [HttpGet("UserEmail/{email}/{entrepriseId:Guid}")]
        public async Task<ActionResult<Secrets>> GetUserEmail([FromRoute] string email, Guid entrepriseId)
        {
            try
            {
                var user = await _userRepository.Item.GetBy(x => x.Email == email && x.EntrepriseId == entrepriseId);
                if (user.Count() != 0)
                {
                    var token = await GenerateAccessToken(user.First().Id);
                    return new Secrets()
                    {
                        Token = token.Token,
                        Prenom = token.Prenom,
                        Nom = token.Nom,
                        TokenExpiry = token.DateOfExpiry,
                        Email = user.First().Email,
                        ProfilePic = user.First().PhotoUrl,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("UserEmail/{email}")]
        public async Task<ActionResult<Secrets>> GetUserEmail([FromRoute] string email)
        {
            try
            {
                var user = await _userRepository.Item.GetBy(x => x.Email == email);
                if (user.Count() != 0)
                {
                    var token = await GenerateAccessToken(user.First().Id);
                    return new Secrets()
                    {
                        Token = token.Token,
                        Prenom = token.Prenom,
                        Email = user.First().Email,
                        Nom = token.Nom,
                        TokenExpiry = token.DateOfExpiry,
                        ProfilePic = user.First().PhotoUrl,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            RefreshToken user = new RefreshToken();
            var refresh = await _refreshTokenRepository.Item.GetBy(x => x.Token == refreshToken
            && (x.Refreshable == "Oui" || x.Refreshable == null));
            if (refresh.Count() != 0)
            {
                user = refresh.First();
                if (user != null && user.DateOfExpiry < DateTime.UtcNow)
                {
                    return true;
                }
            }
            return false;
        }

        private async Task<LogInModel> GetUserFromAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_settings.Key);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var Id = principle.FindFirst(ClaimTypes.Name)?.Value;

                    var user = await _refreshTokenRepository.Item.GetBy(x => x.Token == accessToken);

                    LogInModel model = new LogInModel();
                    model.Token = accessToken;
                    model.Id = (Guid)(user.First().UserId);
                    return model;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.Message.Contains("expir"))
                    {
                        var user = await _refreshTokenRepository.Item.GetBy(x => x.Token == accessToken);
                        LogInModel model = new LogInModel();
                        model.Token = accessToken;
                        model.Id = (Guid)(user.First().UserId);
                        return model;
                    }
                    else return new LogInModel();
                }
                catch (Exception)
                {
                    return new LogInModel();
                }
            }
            return new LogInModel();
        }

        private async Task<TokenDetails> GenerateAccessToken(Guid Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", Convert.ToString(Id)),
                }),
                Audience = _settings.HostName,
                Expires = DateTime.UtcNow.AddHours(_settings.TokenDurationHours),
                IssuedAt = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            await _refreshTokenRepository.Item.AddAsync(new RefreshToken()
            {
                Refreshable = "Oui",
                Token = result,
                UserId = Id,
                DateOfExpiry = Convert.ToDateTime(tokenDescriptor.Expires),
                DateOfIssue = Convert.ToDateTime(tokenDescriptor.IssuedAt),
                RefreshTokenId = Guid.NewGuid(),
            });

            var user = await _userRepository.Item.GetBy(x => x.Id == Id);
            await _refreshTokenRepository.SaveAsync();
            return new TokenDetails()
            {
                DateOfExpiry = Convert.ToDateTime(tokenDescriptor.Expires),
                IssueAt = Convert.ToDateTime(tokenDescriptor.IssuedAt),
                Token = result,
                Prenom = user.First().Prenom,
                Nom = user.First().Nom,
            };
        }
    }
}
