namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientStylesController : GenericController<Style, Client>
    {
        private readonly IGenericRepositoryWrapper<Style, Client> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IFileManager _fileManager;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;
        private readonly IGenericRepositoryWrapper<Gamme, Client, Offre, Marque, Style, Categorie, Taille> _gammeRepos;

        public ClientStylesController(IGenericRepositoryWrapper<Style, Client> wrapper,
            IFileManager fileManager, IGenericRepositoryWrapper<Gamme, Client, Offre, 
                Marque, Style, Categorie, Taille> gammeRepos,
            IConfigSettings settings,
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _gammeRepos = gammeRepos;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _fileManager = fileManager;
            _mapper = mapper;
        }

        
        public override async Task<ActionResult<IEnumerable<Style>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
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

        public override async Task<ActionResult<IEnumerable<Style>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                    (x.EntrepriseId.ToString() == search) && (x.Name.Contains(search)));

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
        public async Task<ActionResult<IEnumerable<Style>>> GetBy(Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x => (x.EntrepriseId == id));
                    foreach (var item in result)
                    {

                    }
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
