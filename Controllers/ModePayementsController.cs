namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModePayementsController : GenericController<ModePayement, User>
    {
        private readonly IGenericRepositoryWrapper<ModePayement, User> repositoryWrapper;
        
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public ModePayementsController(IGenericRepositoryWrapper<ModePayement, User> wrapper,
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<ModePayement>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    ModePayement u = new ModePayement();
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

        public override async Task<ActionResult<ModePayement>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        public override async Task<ActionResult<IEnumerable<ModePayement>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<ModePayement>>> AddAsync([FromBody] List<ModePayement> values)
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

        [HttpGet("{search}/{entreprise:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ModePayement>>> GetBy([FromRoute] string search, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId == entrepriseId)
                    && x.Name.Equals(search));

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("entreprise/{id:Guid}")]
        public async Task<ActionResult<IEnumerable<ModePayement>>> GetBy(Guid id)
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
                        List<EntrepriseUser> list = new List<EntrepriseUser>();
                        foreach (var item in entreprise)
                        {
                            list.Add(item);
                        }

                        if (list.Where(x => x.UserId == identity.First().Id).Count() != 0)
                        {
                            if (list.First().Role == UserRole.Admin)
                            {
                                var result = await repositoryWrapper.Item.GetBy(x => (x.EntrepriseId == id) && x.MadeForAdmin == true);
                                return Ok(result.OrderBy(x => x.Name));
                            }
                            else if (list.First().Role == UserRole.Agent)
                            {
                                var result = await repositoryWrapper.Item.GetBy(x => (x.EntrepriseId == id) && x.MadeForAgent == true);
                                return Ok(result.OrderBy(x => x.Name));
                            }
                            else
                            {
                                var result = await repositoryWrapper.Item.GetBy(x => (x.EntrepriseId == id));
                                return Ok(result.OrderBy(x => x.Name));
                            }
                        }
                        else return NotFound("Non membre cette entreprise");
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

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<ModePayement>>> GetById(Guid id)
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
                        List<EntrepriseUser> list = new List<EntrepriseUser>();
                        foreach (var item in entreprise)
                        {
                            list.Add(new EntrepriseUser()
                            {
                                UserId = (Guid)item.UserId
                            });
                        }

                        if (list.Contains(new EntrepriseUser() { UserId = (Guid)identity.First().Id, }))
                        {
                            var result = await repositoryWrapper.Item.GetBy(x => (x.Id == id));
                            return Ok(result.OrderBy(x => x.Name));
                        }
                        else return NotFound("Non membre cette entreprise");
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
    }
}
