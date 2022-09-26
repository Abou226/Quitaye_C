namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientHeuresController : GenericController<Heure, Client>
    {
        private readonly IGenericRepositoryWrapper<Heure, Client> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<Entreprise> _entrepriseRepos;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public ClientHeuresController(IGenericRepositoryWrapper<Heure, Client> wrapper,
            IConfigSettings settings, 
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, 
            IGenericRepositoryWrapper<Entreprise> entrepriseRepos,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseRepos = entrepriseRepos;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
        }

        

        public override async Task<ActionResult<IEnumerable<Heure>>> GetBy(string search)
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
        public async Task<ActionResult<IEnumerable<Model>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x => x.EntrepriseId.Equals(id));
                    return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        

        [HttpGet("{date:DateTime}/{entrepriseId:Guid}")]
        public async Task<ActionResult<IEnumerable<Heure>>> GetBy(DateTime date, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    if (date.Date == DateTime.Today.Date)
                    {
                        var entrepriseHourType = await _entrepriseRepos.Item.GetBy(x => x.Id == entrepriseId);
                        if (entrepriseHourType.Count() != 0)
                        {
                            if (string.IsNullOrWhiteSpace(entrepriseHourType.First().TypeHeure))
                            {
                                var result = await repositoryWrapper.ItemA.GetBy(x => x.EntrepriseId == entrepriseId
                                && Convert.ToInt32(x.Name) > DateTime.Now.Hour);

                                return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                            }
                            else 
                            {
                                if (entrepriseHourType.First().TypeHeure.Contains("Ponctuel"))
                                {
                                    var result = await repositoryWrapper.ItemA.GetBy(x => x.EntrepriseId == entrepriseId
                                    && Convert.ToInt32(x.Name) > DateTime.Now.Hour);

                                    return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                                }
                                else if (entrepriseHourType.First().TypeHeure.Contains("Interval"))
                                {
                                    var result = await repositoryWrapper.ItemA.GetBy(x => x.EntrepriseId == entrepriseId
                                    && x.Start.Value.Hour > DateTime.Now.Hour);

                                    return Ok(result.OrderBy(x => Convert.ToInt32(x.Start.Value)));
                                }
                                else return null;
                            }
                        }
                        else return null;
                    }
                    else if (date.Date > DateTime.Today.Date)
                    {
                        var entrepriseHourType = await _entrepriseRepos.Item.GetBy(x => x.Id == entrepriseId);
                        if (entrepriseHourType.Count() != 0)
                        {
                            if (string.IsNullOrWhiteSpace(entrepriseHourType.First().TypeHeure))
                            {
                                var result = await repositoryWrapper.ItemA.GetBy(x => x.EntrepriseId == entrepriseId);

                                return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                            }
                            else if (entrepriseHourType.First().TypeHeure.Contains("Pontuel"))
                            {
                                var result = await repositoryWrapper.ItemA.GetBy(x => x.EntrepriseId == entrepriseId);

                                return Ok(result.OrderBy(x => Convert.ToInt32(x.Name)));
                            }
                            else if (entrepriseHourType.First().TypeHeure.Contains("Interval"))
                            {
                                var result = await repositoryWrapper.ItemA.GetBy(x => x.EntrepriseId == entrepriseId);

                                return Ok(result.OrderBy(x => Convert.ToInt32(x.Start.Value.Hour)));
                            }
                            else return null;
                        }
                        else return null;
                    }
                    else return null;
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erreur serveur");
            }
        }
    }
}
