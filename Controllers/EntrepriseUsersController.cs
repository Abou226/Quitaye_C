﻿namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EntrepriseUsersController : GenericController<EntrepriseUser, User, User>
    {
        private readonly IGenericRepositoryWrapper<EntrepriseUser, User, User> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public EntrepriseUsersController(IGenericRepositoryWrapper<EntrepriseUser, User, User> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<EntrepriseUser>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    EntrepriseUser u = new EntrepriseUser();
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

        public override async Task<ActionResult<EntrepriseUser>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid id)
        {
            try
            {
                var item = await repositoryWrapper.Item.GetBy(x => x.Id == id);
                if (item.Count() != 0)
                {
                    var single = item.First();
                    value.ApplyTo(single);
                    await repositoryWrapper.SaveAsync();
                }
                else return NotFound("User not indentified");

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<EntrepriseUser>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<EntrepriseUser>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId.ToString() == search) && (x.User.Prenom.Contains(search)));

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<EntrepriseUser>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var entreprise = await _entrepriseUser.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entreprise.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entreprise)
                        {
                            list.Add((Guid)item.UserId);
                        }

                        if (list.Contains(identity.First().Id))
                        {
                            var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id), x => x.User);
                            return Ok(result);
                        }
                        else return NotFound("Non membre de cette entreprise");
                    }
                    else return NotFound("Non membre de cette entreprise");
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<EntrepriseUser>>> AddAsync([FromBody] List<EntrepriseUser> values)
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
                    //value.UserId = identity.First().Id;
                    foreach (var value in values)
                    {
                        value.Id = Guid.NewGuid();
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

        public override async Task<ActionResult<IEnumerable<EntrepriseUser>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId.ToString() == search) && (x.User.Prenom.Contains(search)));

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
