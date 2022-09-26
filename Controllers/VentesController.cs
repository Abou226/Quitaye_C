namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentesController : GenericController<Vente, User, 
        Offre, Marque, Taille, Model, Style, Categorie, Niveau, List<OccasionList>>
    {
        private readonly IGenericRepositoryWrapper<Vente, User, 
            Offre, Marque, Taille, Model, Style, Categorie, Niveau, List<OccasionList>> repositoryWrapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUserRepository;
        private readonly IConfigSettings _settings;
        private readonly IGenericRepositoryWrapper<PanierVente> panierRepository;
        private readonly IGenericRepositoryWrapper<Num_Vente> num_vente_repository;
        private readonly IMapper _mapper;

        public VentesController(IGenericRepositoryWrapper<Vente, User, Offre, 
            Marque, Taille, Model, Style, Categorie, Niveau, List<OccasionList>> wrapper,
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUserRepository, 
            IGenericRepositoryWrapper<PanierVente> _panierRepository, 
            IGenericRepositoryWrapper<Num_Vente> _num_vente_repository,
            IConfigSettings settings, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            num_vente_repository = _num_vente_repository;
            _entrepriseUserRepository = entrepriseUserRepository;
            _panierRepository = panierRepository;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Vente>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    Vente u = new Vente();
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

        public override async Task<ActionResult<Vente>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        public override async Task<ActionResult<IEnumerable<Vente>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<Vente>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                    (x.EntrepriseId == identity.FirstOrDefault().EntrperiseId)
                    && (x.Client.Prenom.Contains(search) || x.Client.Nom.Contains(search)
                    || x.Contact_Livraison.Contains(search) || x.Heure_Livraison.Contains(search)
                    || x.Offre.Marque.Name.Contains(search) || x.Offre.Style.Name.Contains(search)
                    || x.Offre.Categorie.Name.Contains(search)) && x.Annulée == false, 
                    x => x.Offre,
                    x => x.Offre.Marque, 
                    x => x.Offre.Taille, 
                    x => x.Offre.Model, 
                    x => x.Offre.Style,
                    x => x.Offre.Categorie, 
                    x => x.Offre.Niveau, 
                    x => x.Offre.Occasionss);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public override async Task<ActionResult<IEnumerable<Vente>>> AddAsync([FromBody] List<Vente> values)
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
                    
                    Num_Vente numss = new Num_Vente();
                    numss.Id = Guid.NewGuid();
                    numss.EntrepriseId = identity.First().Id;
                    Random rd = new Random();
                    string num = "";
                    string nums = "";
                    string mois = DateTime.Now.ToString("MM");
                    string année = DateTime.Now.ToString("yy");
                    string jour = DateTime.Now.ToString("dd");
                    int c = 1;
                    int a = rd.Next(0, 26);
                    char ch = (char)('a' + a);
                    string ord = "";
                    while (c != 0)
                    {
                        num = rd.Next(10000, 100000).ToString();
                        nums = rd.Next(10000, 100000).ToString();
                        ord = jour + mois + année + "-" + DateTime.Now.ToString("hh:mm") + ch + "." + num + "." + nums;
                        var ser = await num_vente_repository.Item.GetBy(x => x.Name == ord.ToUpper());
                        c = ser.Count();
                    }
                    numss.Name = ord.ToUpper();
                    numss.Entreprise = identity.First().Entreprise;
                    numss.Date = DateTime.Now;
                    await num_vente_repository.Item.AddAsync(numss);
                    await repositoryWrapper.SaveAsync();
                    var num_vente = await num_vente_repository.Item.AddAsync(numss);
                    foreach (var item in values)
                    {
                        if (item.Date == Convert.ToDateTime("0001-01-01T00:00:00"))
                            item.Date = DateTime.Now;
                        item.ServerTime = DateTime.Now;
                        item.UserId = identity.First().Id;
                        item.Id = Guid.NewGuid();
                        item.Num_VenteId = num_vente.Id;
                        await repositoryWrapper.ItemA.AddAsync(item);
                        await repositoryWrapper.SaveAsync();
                        var pan = await panierRepository.Item.GetBy(x => x.Id == item.PanierId);
                         panierRepository.Item.Delete(pan.First());
                        await panierRepository.SaveAsync();
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

        [HttpGet("{entrepriseId:Guid}/{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Vente>>> GetBy([FromRoute] Guid entrepriseId, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var entrepriseUser = await _entrepriseUserRepository.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entrepriseUser.Count() != 0)
                    {
                        var result = await repositoryWrapper.Item.GetByInclude(x =>
                        (x.EntrepriseId == entrepriseId)
                        && x.Date.Date >= start.Date && x.Date.Date <= end.Date,
                        x => x.Offre,
                        x => x.Offre.Marque, 
                        x => x.Offre.Taille,
                        x => x.Offre.Model, 
                        x => x.Offre.Style, 
                        x => x.Offre.Categorie, 
                        x => x.Offre.Niveau, 
                        x => x.Offre.Occasionss);

                        return Ok(result);
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

        [HttpGet("Resume/{entrepriseId:Guid}/{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Vente>>> GetForChart([FromRoute] Guid entrepriseId, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var entrepriseUser = await _entrepriseUserRepository.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entrepriseUser.Count() != 0)
                    {
                        var result = await repositoryWrapper.Item.GetBy(x =>
                        (x.EntrepriseId == entrepriseId)
                        && x.Date.Date >= start && x.Date <= end && x.Annulée == false);

                        var charts = new List<ChartData>();
                        foreach (var item in result.OrderBy(x => x.Date).GroupBy(x => x.Date.Date))
                        {
                            charts.Add(new ChartData()
                            {
                                Date = item.Key.Date,
                                Montant = item.Sum(x => x.Prix_Unité),
                            });
                        }
                        return Ok(charts);
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


        [HttpGet("{entrepriseId:Guid}/{search}/{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Vente>>> GetBy([FromRoute] Guid entrepriseId, string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var entrepriseUser = await _entrepriseUserRepository.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entrepriseUser.Count() != 0)
                    {
                        var result = await repositoryWrapper.Item.GetByInclude(x =>
                        (x.EntrepriseId == entrepriseId)
                        && x.Date.Date >= start.Date && x.Date.Date <= end.Date && x.Annulée == false,
                        x => x.Offre,
                        x => x.Offre.Marque, 
                        x => x.Offre.Taille,
                        x => x.Offre.Model, 
                        x => x.Offre.Style, 
                        x => x.Offre.Categorie,
                        x => x.Offre.Niveau, 
                        x => x.Offre.Occasionss) ;

                        return Ok(result);
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
    }
}
