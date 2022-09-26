namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientTaillesController : GenericController<Taille, Client, Categorie>
    {
        private readonly IGenericRepositoryWrapper<Taille, Client, Categorie> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<Offre, Client, Taille> _offreRepos;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public ClientTaillesController(IGenericRepositoryWrapper<Taille, Client, Categorie> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, 
            IGenericRepositoryWrapper<Offre, Client, Taille> offreRepos,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _offreRepos = offreRepos;
            _settings = settings;
            _mapper = mapper;
        }

        public override async Task<ActionResult<IEnumerable<Taille>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<Taille>>> GetBy(string search)
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

                    return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<Taille>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id), x => x.Categorie);
                    return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("min/{offreId:Guid}/{id:Guid}")]

        public async Task<ActionResult<IEnumerable<Taille>>> GetByWithMinTaille([FromRoute] Guid offreId, Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var offres = await _offreRepos.Item.GetByInclude(x => x.Id == offreId, x => x.Taille);
                    if (offres.Count() != 0)
                    {
                        var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id && 
                        Convert.ToInt32(x.Name) >= Convert.ToInt32(offres.First().Taille.Name)), x => x.Categorie);
                        return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                    }
                    else return null;
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        public override async Task<ActionResult<IEnumerable<Taille>>> GetBy(string search, DateTime start, DateTime end)
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

                    return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
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
