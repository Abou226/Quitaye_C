
namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : GenericController<Models.Client, User>
    {
        private readonly IGenericRepositoryWrapper<Models.Client, User> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public ClientsController(IGenericRepositoryWrapper<Models.Client, User> wrapper,
            IConfigSettings settings, 
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, 
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Models.Client>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    Models.Client u = new Models.Client();
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

        public override async Task<ActionResult<Models.Client>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        [HttpPatch("base")]
        public async Task<ActionResult<Models.Client>> PatchUpdateAsync([FromBody] JsonPatchDocument value)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var single = identity.First();
                    value.ApplyTo(single);
                    await repositoryWrapper.SaveAsync();
                    return Ok(value);
                }
                else return NotFound("Client not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("Email/{email}")]
        public async Task<ActionResult<Models.Client>> GetUser([FromRoute] string email)
        {
            try
            {
                //var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                //var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                //Equals(claim));
                //if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemA.GetBy(x => (x.Email == email));
                    if (result.Count() != 0)
                    {
                        Models.Client client = new Models.Client();
                        client.Email = result.First().Email;
                        return Ok(client);
                    }
                    else return null;
                }
                //else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       
        [HttpGet("baseinfo")]
        public async Task<ActionResult<Models.Client>> GetUserBaseInfo()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    Models.Client client = new Models.Client();
                    client.Email = identity.First().Email;
                    client.Prenom = identity.First().Prenom;
                    client.Nom = identity.First().Nom;
                    client.Telephone = identity.First().Telephone;
                    client.PhotoUrl = identity.First().PhotoUrl;
                    return Ok(client);
                }
                else return NotFound("Client not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        public override async Task<ActionResult<IEnumerable<Models.Client>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemA.GetAll();

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Models.Client>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId.ToString() == search) && (x.Nom.Contains(search) 
                    || x.Prenom.Contains(search)));

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
        public async Task<ActionResult<IEnumerable<Models.Client>>> GetBy([FromRoute] Guid id)
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
                            var result = await repositoryWrapper.Item.GetBy(x => (x.EntrepriseId == id));
                            return Ok(result);
                        }
                        else
                            return NotFound("Non membre de cette entreprise");
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

        [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<Models.Client>>> AddAsync([FromBody] List<Models.Client> values)
        {
            try
            {
                if (values == null)
                    return NotFound();

                foreach (var value in values)
                {
                    if (!string.IsNullOrWhiteSpace(value.Username))
                    {
                        var u = await repositoryWrapper.ItemA.GetBy(x => x.Username == value.Username);
                        if (u.Count() != 0)
                            return BadRequest("Nom d'utilisateur deja existant, veiller choisir un nom d'utilisateur unique");
                        {
                            await Add(value);
                        }
                    }
                    else
                    {
                        var u = await repositoryWrapper.ItemA.GetBy(x => x.Email == value.Email);
                        if (u.Count() != 0)
                            return BadRequest("Nom d'utilisateur deja existant, veiller choisir un nom d'utilisateur unique");
                        {
                            await Add(value);
                        }
                    }
                }
                //else return NotFound();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        async Task Add(Models.Client value)
        {
            value.Id = Guid.NewGuid();
            if (value.DateOfCreation == Convert.ToDateTime("0001-01-01T00:00:00"))
                value.DateOfCreation = DateTime.Now;
            //value.ServerTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(value.Password))
                value.Password = _settings.PaswordEncryption(value.Password + _settings.Key);
            await repositoryWrapper.ItemA.AddAsync(value);
            await repositoryWrapper.SaveAsync();
        }


        [HttpGet("{entrepriseId:Guid}/{search}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Models.Client>>> GetBy([FromRoute] Guid entrepriseId, string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId == entrepriseId) && (x.Nom.Contains(search) || x.Prenom.Contains(search)));

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
