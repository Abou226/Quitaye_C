namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : GenericController<Reservation, User, Offre,
        Gamme, Marque, Taille, Model, Categorie, Style, Marque, Niveau, List<OccasionList>,
        Marque, Categorie, Style, Model, Client, Num_Vente>
    {
        private readonly IGenericRepositoryWrapper<Reservation, User, Offre,
        Gamme, Marque, Taille, Model, Categorie, Style, Marque, Niveau, List<OccasionList>,
        Marque, Categorie, Style, Model, Client, Num_Vente> repositoryWrapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUserRepository;
        private readonly IConfigSettings _settings;
        private readonly IGenericRepositoryWrapper<PanierReservation> panierRepository;
        private readonly IGenericRepositoryWrapper<Num_Vente> num_vente_repository;
        private readonly IMapper _mapper;

        public ReservationsController(IGenericRepositoryWrapper<Reservation, User, Offre,
        Gamme, Marque, Taille, Model, Categorie, Style, Marque, Niveau, List<OccasionList>,
        Marque, Categorie, Style, Model, Client, Num_Vente> wrapper,
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUserRepository,
            IGenericRepositoryWrapper<PanierReservation> _panierRepository, 
            IGenericRepositoryWrapper<Num_Vente> _num_vente_repository,
            IConfigSettings settings, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            num_vente_repository = _num_vente_repository;
            _entrepriseUserRepository = entrepriseUserRepository;
            panierRepository = _panierRepository;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Reservation>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var entreprise = await _entrepriseUserRepository.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entreprise.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entreprise)
                        {
                            list.Add((Guid)item.UserId);
                        }

                        if (list.Contains(identity.First().Id))
                        {
                            Reservation u = new Reservation();
                            u.Id = id;
                            repositoryWrapper.Item.Delete(u);
                            await repositoryWrapper.SaveAsync();
                            return Ok(u);
                        }
                        else return NotFound("Non membre de cette entreprise");
                    }
                    else return NotFound("Non membre de cette entreprise");
                }
                else return NotFound("Utilisateur non identifier");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<Reservation>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var entreprise = await _entrepriseUserRepository.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entreprise.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entreprise)
                        {
                            list.Add((Guid)item.UserId);
                        }
                        if (list.Contains(identity.First().Id))
                        {
                            var item = await repositoryWrapper.Item.GetBy(x => x.Id == id);
                            if (item.Count() != 0)
                            {
                                var single = item.First();
                                value.ApplyTo(single);
                                await repositoryWrapper.SaveAsync();
                            }
                            else return NotFound("Element not found");

                            return Ok(value);
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

        [HttpPatch("entreprise/{entreprise:Guid}")]
        public async Task<ActionResult<Reservation>> ChangeEntrepriseUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid entreprise)
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

        public override async Task<ActionResult<IEnumerable<Reservation>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<Reservation>>> GetBy(string search)
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
                                || x.Gamme.Marque.Name.Contains(search) 
                                || x.Gamme.Style.Name.Contains(search)
                                || x.Gamme.Categorie.Name.Contains(search)) && x.Annulée == false,
                                x => x.Offre,
                                x => x.Gamme,
                                x => x.Offre.Marque,
                                x => x.Taille,
                                x => x.Model,
                                x => x.Offre.Categorie,
                                x => x.Offre.Style,
                                x => x.Marque,
                                x => x.Offre.Niveau,
                                x => x.Offre.Occasionss,
                                x => x.Gamme.Marque,
                                x => x.Gamme.Categorie,
                                x => x.Gamme.Style,
                                x => x.Offre.Model,
                                x => x.Client,
                                x => x.Num_Vente);

                    return Ok(result.OrderByDescending(x => x.DateOfCreation));
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
        public override async Task<ActionResult<IEnumerable<Reservation>>> AddAsync([FromBody] List<Reservation> value)
        {
            try
            {
                if (value == null)
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
                    numss.EntrepriseId = value.First().EntrepriseId;
                    numss.Date = DateTime.Now;
                    await num_vente_repository.Item.AddAsync(numss);
                    await num_vente_repository.SaveAsync();
                    foreach (var item in value)
                    {
                        if (item.DateOfCreation == Convert.ToDateTime("0001-01-01T00:00:00"))
                            item.DateOfCreation = DateTime.Now;
                        item.ServerTime = DateTime.Now;
                        item.Id = Guid.NewGuid();
                        item.NumVenteId = numss.Id;
                        item.UserId = identity.First().Id;
                        await repositoryWrapper.ItemA.AddAsync(item);
                        await repositoryWrapper.SaveAsync();

                        var pan = await panierRepository.Item.GetBy(x => x.Id == item.PanierId);
                        if (pan.Count() != 0)
                        {
                            panierRepository.Item.Delete(pan.First());
                            await panierRepository.SaveAsync();
                        }
                    }

                    //if(value.Count == 1)
                    //{
                    //    Notification notification = new Notification();
                    //    notification.Title = "Nouvelle Commande";
                    //    var marque = "";
                    //    if (value.First().Gamme != null)
                    //    {
                    //        if (value.First().Gamme.Marque != null)
                    //            marque = value.First().Gamme.Marque.Name;
                    //        else mar
                    //    }
                    //    else marque = value.First().Marque.Name;

                    //    notification.Message = $"{marque}-{value.First().Taille.Name}, {value.First().Model.Name}. Client(e): {identity.First().Prenom} {identity.First().Nom}";
                    //    var notif = await notificationService.SendToTopic($"{value.First().EntrepriseId.ToString()}_vente", notification);
                    //    notification.AuthorId = identity.First().Id;
                    //    notification.SendDate = DateTime.Now;
                    //    notification.Url = value.First().Gamme.Url;
                    //    var result = await notifRepository.Item.AddAsync(notification);
                    //    await notifRepository.SaveAsync();
                    //}else
                    //{
                    //    Notification notification = new Notification();
                    //    notification.Title = $"{value.Count} Nouvelles Commandes";
                    //    var marque = "";
                    //    if (value.First().Gamme.Marque != null)
                    //        marque = value.First().Gamme.Marque.Name;
                    //    else marque = value.First().Marque.Name;

                    //    notification.Message = $"Client(e): {identity.First().Prenom} {identity.First().Nom} Montant: {Convert.ToDecimal(value.Sum(x => x.Prix_Vente_Unité)).ToString("N0")}";
                    //    var notif = await notificationService.SendToTopic($"{value.First().EntrepriseId.ToString()}_vente", notification);
                    //    notification.AuthorId = identity.First().Id;
                    //    notification.SendDate = DateTime.Now;
                    //    notification.Url = value.First().Gamme.Url;
                    //    var result = await notifRepository.Item.AddAsync(notification);
                    //    await notifRepository.SaveAsync();
                    //}
                    return Ok(value);
                }
                else return NotFound("User not identified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetBy([FromRoute] DateTime start, DateTime end, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var entreprise = await _entrepriseUserRepository.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entreprise.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entreprise)
                        {
                            list.Add((Guid)item.UserId);
                        }

                        if (list.Contains(identity.First().Id))
                        {
                            var result = await repositoryWrapper.Item.GetByInclude(x =>
                                        (x.EntrepriseId == entrepriseId)
                                        && x.DateOfCreation.Date >= start.Date 
                                        && x.DateOfCreation.Date <= end.Date 
                                        && x.Annulée == false,
                                        x => x.Offre,
                                        x => x.Gamme,
                                        x => x.Offre.Marque,
                                        x => x.Taille,
                                        x => x.Model,
                                        x => x.Offre.Categorie,
                                        x => x.Offre.Style,
                                        x => x.Marque,
                                        x => x.Offre.Niveau,
                                        x => x.Offre.Occasionss,
                                        x => x.Gamme.Marque,
                                        x => x.Gamme.Categorie,
                                        x => x.Gamme.Style,
                                        x => x.Offre.Model,
                                        x => x.Client,
                                        x => x.Num_Vente);
                            return Ok(result.OrderByDescending(x => x.DateOfCreation));
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

        [HttpGet("num_vente/{num_vente}/{entreprise:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetByNum_Vente([FromRoute] string num_vente, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var entreprise = await _entrepriseUserRepository.Item.GetBy(x => x.UserId == identity.First().Id);
                    if (entreprise.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entreprise)
                        {
                            list.Add((Guid)item.UserId);
                        }

                        if (list.Contains(identity.First().Id))
                        {
                            var result = await repositoryWrapper.Item.GetByInclude(x =>
                                        (x.EntrepriseId == entrepriseId 
                                        && x.Num_Vente.Name == num_vente)
                                        && x.Annulée == false,
                                        x => x.Offre,
                                        x => x.Gamme,
                                        x => x.Offre.Marque,
                                        x => x.Taille,
                                        x => x.Model,
                                        x => x.Offre.Categorie,
                                        x => x.Offre.Style,
                                        x => x.Marque,
                                        x => x.Offre.Niveau,
                                        x => x.Offre.Occasionss,
                                        x => x.Gamme.Marque,
                                        x => x.Gamme.Categorie,
                                        x => x.Gamme.Style,
                                        x => x.Offre.Model,
                                        x => x.Client,
                                        x => x.Num_Vente);
                            return Ok(result.OrderByDescending(x => x.DateOfCreation));
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

        [HttpGet("result/{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetForChart([FromRoute] DateTime start, DateTime end, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var entreprise = await _entrepriseUserRepository.Item.GetBy(x => x.EntrepriseId == entrepriseId);
                    if (entreprise.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entreprise)
                        {
                            list.Add((Guid)item.UserId);
                        }

                        if (list.Contains(identity.First().Id))
                        {
                            var result = await repositoryWrapper.Item.GetBy(x =>
                                (x.EntrepriseId == entrepriseId)
                                && x.DateOfCreation.Date >= start.Date 
                                && x.DateOfCreation.Date <= end.Date 
                                && x.Annulée == false);

                            var charts = new List<ChartData>();
                            foreach (var item in result.OrderBy(x => x.DateOfCreation).GroupBy(x => x.DateOfCreation.Date))
                            {
                                charts.Add(new ChartData()
                                {
                                    Date = item.Key.Date,
                                    Quantité = item.Sum(x => x.Quantité),
                                    Montant = item.Sum(x => x.Prix_Vente_Unité),
                                });
                            }
                            return Ok(charts);
                        }
                        else return NotFound("Non membre cette entreprise");
                    }
                    else return NotFound("Non trouvé");
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("livraison/{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetLivraison([FromRoute] DateTime start, DateTime end, Guid entrepriseId)
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
                                && x.Date_Livraison.Date >= start.Date
                                && x.Date_Livraison.Date <= end.Date
                                && x.Annulée == false,
                                x => x.Offre,
                                x => x.Gamme,
                                x => x.Offre.Marque,
                                x => x.Taille,
                                x => x.Model,
                                x => x.Offre.Categorie,
                                x => x.Offre.Style,
                                x => x.Marque,
                                x => x.Offre.Niveau,
                                x => x.Offre.Occasionss,
                                x => x.Gamme.Marque,
                                x => x.Gamme.Categorie,
                                x => x.Gamme.Style,
                                x => x.Offre.Model,
                                x => x.Client,
                                x => x.Num_Vente);

                    return Ok(result.OrderByDescending(x => x.Date_Livraison));
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
