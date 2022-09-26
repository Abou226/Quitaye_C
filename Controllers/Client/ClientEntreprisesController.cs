namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientEntreprisesController : GenericController<Entreprise, Client, 
        Type_Entreprise, Quartier, EntrepriseUser>
    {
        private readonly IGenericRepositoryWrapper<Entreprise, 
            Client, Type_Entreprise, Quartier, 
            EntrepriseUser> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;

        public ClientEntreprisesController(IGenericRepositoryWrapper<Entreprise, 
            Client, Type_Entreprise, Quartier, EntrepriseUser> wrapper,
            IConfigSettings settings, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                //var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                //var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                //Equals(claim));
                //if (identity.Count() != 0)
                {
                    var entreprise = await repositoryWrapper.Item.GetByInclude(x => x.Id == id, x => x.Type, x => x.Quartier);
                    return Ok(entreprise.First());
                }
                //else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
