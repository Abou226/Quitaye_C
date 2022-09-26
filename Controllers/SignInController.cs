namespace Controllers
{
    [Route("signin-google")]
    [ApiController]
    public class SignInController : ControllerBase
	{
		const string callbackScheme = "quitaye";

		private readonly string scheme;
		private readonly IGenericRepositoryWrapper<User> userRepos;
		private readonly IFacebook _facebook;
		public SignInController(IGenericRepositoryWrapper<User> userRepos, IFacebook facebook)
		{
			this.userRepos = userRepos;
			_facebook = facebook;
			scheme = "Google";
		}

		[HttpGet]
		public async Task GetBy()
		{
			var auth = await Request.HttpContext.AuthenticateAsync(scheme);

			if (!auth.Succeeded
				|| auth?.Principal == null
				|| !auth.Principal.Identities.Any(id => id.IsAuthenticated)
				|| string.IsNullOrEmpty(auth.Properties.GetTokenValue("access_token")))
			{
				// Not authenticated, challenge
				await Request.HttpContext.ChallengeAsync(scheme);
				//return	Ok(null);
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

				// Get parameters to send back to the callback
				var qs = new Dictionary<string, string>
				{
					{ "access_token", auth.Properties.GetTokenValue("access_token") },
					{ "refresh_token", auth.Properties.GetTokenValue("refresh_token") ?? string.Empty },
					{ "expires", (auth.Properties.ExpiresUtc?.ToUnixTimeSeconds() ?? -1).ToString() },
					{ "email", email }
				};

				// Build the result url
				var url = callbackScheme + "://#" + string.Join(
					"&",
					qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
					.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

				//var user =await userRepos.Item.GetBy(x => x.Email == email );
				//if(user.Count() == 0)
				//            {
				//	var u = await userRepos.Item.AddAsync(new Models.User
				//	{
				//		Id = Guid.NewGuid(),
				//		Email = email,
				//		Active = "Oui",
				//		DateOfCreation = DateTime.Now,
				//		Prenom = surName,
				//		PhotoUrl = picture,
				//		Nom = givenName
				//	});
				//            }
				// Redirect to final url
				Request.HttpContext.Response.Redirect(url);
				//return	Ok(null);
			}

		}
	}
}
