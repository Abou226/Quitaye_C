namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserInvitationsController : GenericController<UserInvitation, User, Entreprise, User, Quartier, Ville, Pays>
    {
        private readonly IGenericRepositoryWrapper<UserInvitation, User, Entreprise, User, Quartier, Ville, Pays> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public UserInvitationsController(IGenericRepositoryWrapper<UserInvitation, User, Entreprise, User, Quartier, Ville, Pays> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<UserInvitation>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    UserInvitation u = new UserInvitation();
                    u.Id = id;
                    repositoryWrapper.Item.Delete(u);
                    await repositoryWrapper.SaveAsync();
                    return Ok(u);
                }
                else return NotFound("Utilisateur non identifier");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("entreprise/{entreprise:Guid}")]
        public async Task<ActionResult<Heure>> ChangeEntrepriseUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid entreprise)
        {
            try
            {
                var item = await repositoryWrapper.Item.GetBy(x => x.EntrepriseId == entreprise);
                if (item.Count() != 0)
                {
                    foreach (var items in item)
                    {
                        var single = items;
                        value.ApplyTo(single);
                        await repositoryWrapper.SaveAsync();
                    }
                }
                else return NotFound("User not indentified");

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<UserInvitation>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid id)
        {
            try
            {
                var item = await repositoryWrapper.Item.GetBy(x => x.Id == id);
                if (item.Count() != 0)
                {
                    var single = item.First();
                    value.ApplyTo(single);
                    await repositoryWrapper.SaveAsync();
                    var ites = await repositoryWrapper.Item.GetBy(x => x.Id == id);
                    if(ites.Count() != 0)
                    {
                        if (ites.First().Accepted)
                        {
                            var user = await repositoryWrapper.ItemB.GetBy(x => x.Email == ites.First().Email);
                            var us = await _entrepriseUser.Item.GetBy(x => 
                            x.UserId == user.First().Id 
                            && x.EntrepriseId == ites.First().EntrepriseId);
                            if(us.Count() == 0)
                            {
                                var add = await _entrepriseUser.Item.AddAsync(new EntrepriseUser()
                                {
                                    EntrepriseId = ites.First().EntrepriseId,
                                    UserId = user.First().Id,
                                    DateOfAdd = DateTime.Now,
                                    Confirmed = true,
                                    Id = Guid.NewGuid(),
                                    Role = UserRole.Agent,
                                });
                                await _entrepriseUser.SaveAsync();
                            }
                        }
                    }
                }
                else return NotFound("User not indentified");

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<UserInvitation>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetAll();

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<UserInvitation>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId.ToString() == search) 
                    && (x.Email.Contains(search)));

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{entrepriseId:Guid}")]
        public async Task<ActionResult<IEnumerable<UserInvitation>>> GetBy([FromRoute] Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x => (x.EntrepriseId == entrepriseId));
                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserInvitation>> GetByEmail([FromRoute] string email)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {   
                    var result = await repositoryWrapper.Item.GetByInclude(x => x.Email == email 
                                && x.Confirmed == false, 
                                x => x.Entreprise, 
                                x => x.User, 
                                x => x.Entreprise.Quartier, 
                                x => x.Entreprise.Ville, 
                                x => x.Entreprise.Ville.Pays);
                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<UserInvitation>>> AddAsync([FromBody] List<UserInvitation> values)
        {
            try
            {
                if (values == null)
                    return NotFound();

                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    foreach (var value in values)
                    {
                        value.Id = Guid.NewGuid();
                        value.InviterId = identity.First().Id;
                        value.EntrepriseId = value.EntrepriseId;
                        await repositoryWrapper.ItemA.AddAsync(value);
                        await repositoryWrapper.SaveAsync();
                    }
                    return Ok(values);
                }
                else return NotFound("User not identified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<UserInvitation>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId.ToString() == search) && (x.Email.Contains(search)));

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EntrepriseUser>>> GetBy([FromRoute] DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetAll();
                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
