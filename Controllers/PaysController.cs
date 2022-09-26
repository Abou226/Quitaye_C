namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaysController : GenericController<Pays, User, Continent>
    {
        private readonly IGenericRepositoryWrapper<Pays, User, Continent> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public PaysController(IGenericRepositoryWrapper<Pays, User, Continent> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Pays>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    Pays u = new Pays();
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

        public override async Task<ActionResult<Pays>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        public override async Task<ActionResult<IEnumerable<Pays>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemA.GetAll();

                    return Ok(result.OrderBy(x => x.NameEn));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Pays>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.Id.ToString() == search) && (x.Name.Contains(search)));

                    return Ok(result.OrderBy(x => x.Name));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<Pays>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x => (x.Id == id));
                    return Ok(result.OrderBy(x => x.Name));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Pays>>> AddAsync([FromBody] List<Pays> values)
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
                    
                    var lines = System.IO.File.ReadAllLines("stri", System.Text.Encoding.UTF7);
                    foreach (var item in lines)
                    {
                        var spli = item.Split(',');
                        var p = await repositoryWrapper.Item.GetBy(x => x.Alpha_2 == spli[2] || x.Alpha_3 == spli[3]);
                        if(p.Count() == 0)
                        {
                            var pay = new Pays();
                            pay.Id = Guid.NewGuid();
                            pay.Name = spli[4];
                            pay.NameEn = spli[5];
                            pay.Alpha_2 = spli[2];
                            pay.Alpha_3 = spli[3];
                            pay.Indicatif = Convert.ToInt32(spli[1]);
                            await repositoryWrapper.ItemA.AddAsync(pay);
                            await repositoryWrapper.SaveAsync();
                        }
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

        [AllowAnonymous]
        [HttpPost("AllContries")]
        public async Task<ActionResult<Pays>> AddAllContriesAsync([FromForm] Pays value)
        {
            try
            {
                if (value == null)
                    return NotFound();

                var lines = System.IO.File.ReadAllLines(@"C:\Users\Aboubacar Traore\Downloads\sql_pays.csv", System.Text.Encoding.UTF7);
                foreach (var item in lines)
                {
                    var spli = item.Split(',');
                    var p = await repositoryWrapper.Item.GetBy(x => x.Alpha_2 == spli[2].Trim(new Char[] { '"' }) || x.Alpha_3 == spli[3].Trim(new Char[] { '"' }));
                    if (p.Count() == 0)
                    {

                        var pay = new Pays();
                        pay.Id = Guid.NewGuid();
                        pay.Name = spli[4].Trim(new Char[] { '"' });
                        
                        pay.NameEn = spli[5].Trim(new Char[] { '"' });
                        pay.Alpha_2 = spli[2].Trim(new Char[] { '"' });
                        pay.Alpha_3 = spli[3].Trim(new Char[] { '"' });
                        pay.Indicatif = Convert.ToInt32(spli[1].Trim(new Char[] { '"' }));
                        await repositoryWrapper.ItemA.AddAsync(pay);
                        await repositoryWrapper.SaveAsync();
                    }
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Pays>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.Id.ToString() == search) && (x.Name.Contains(search)));

                    return Ok(result.OrderBy(x => x.Name));
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
        public async Task<ActionResult<IEnumerable<Pays>>> GetBy([FromRoute] DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetAll();

                    return Ok(result.OrderBy(x => x.Name));
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
