namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Matière_PremieresController : GenericController<Matière_Premiere, User, UnitéMatière>
    {
        private readonly IGenericRepositoryWrapper<Matière_Premiere, User, UnitéMatière> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public Matière_PremieresController(IGenericRepositoryWrapper<Matière_Premiere, User, UnitéMatière> wrapper, 
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, 
            IFileManager fileManager, 
            IConfigSettings settings, 
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            _fileManager = fileManager;
            _entrepriseUser = entrepriseUser;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Matière_Premiere>> Delete([FromRoute] Guid id)
        {
            Matière_Premiere u = new Matière_Premiere();

            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    u.Id = id;
                    repositoryWrapper.Item.Delete(u);
                    await repositoryWrapper.SaveAsync();
                    return Ok(u);
                }
                else return NotFound("Utilisateur non identifier");
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("conflic"))
                {
                    var dard = new JsonPatchDocument();
                    dard.Add("Op", "Replace");
                    dard.Add("Value", "False");
                    dard.Add("Path", "Active");

                    var item = await repositoryWrapper.Item.GetBy(x => x.Id == id);
                    if (item.Count() != 0)
                    {
                        var single = item.First();
                        dard.ApplyTo(single);
                        await repositoryWrapper.SaveAsync();
                    }
                    else return NotFound("Item not identified");
                    return Ok(u);
                }
                else return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<Matière_Premiere>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        public override async Task<ActionResult<IEnumerable<Matière_Premiere>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<Matière_Premiere>>> AddAsync([FromBody] List<Matière_Premiere> values)
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
                        if (value.Image != null)
                        {
                            var result = await _fileManager.Upload(_settings.AccessKey, _settings.SecretKey, _settings.BucketName, Amazon.RegionEndpoint.USEast1, value.Image);
                            value.Url = result.Url;
                        }
                        value.UserId = identity.First().Id;
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
        public async Task<ActionResult<IEnumerable<Matière_Premiere>>> GetBy([FromRoute] string search, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                    (x.EntrepriseId == entrepriseId) 
                    && x.Unité.Equals(search), x => x.Unité);

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
        public async Task<ActionResult<IEnumerable<Matière_Premiere>>> GetBy(Guid id)
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
                            var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id), x => x.Unité);
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
