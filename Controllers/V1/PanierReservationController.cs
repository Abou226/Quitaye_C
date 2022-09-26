namespace Controllers.V1
{
    [Route("api/v1.1/[controller]")]
    [ApiController]
    [Authorize]
    public class PanierReservationsController : GenericController<PanierReservation,
        User, Offre, Gamme, Marque, Taille, Model, Categorie, Style, Niveau,
        List<OccasionList>, Marque, Categorie, Style, Model, Client>
    {
        private readonly IGenericRepositoryWrapper<PanierReservation,
        User, Offre, Gamme, Marque, Taille, Model, Categorie, Style, Niveau,
        List<OccasionList>, Marque, Categorie, Style, Model, Client> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public PanierReservationsController(IGenericRepositoryWrapper<PanierReservation,
        User, Offre, Gamme, Marque, Taille, Model, Categorie, Style, Niveau,
        List<OccasionList>, Marque, Categorie, Style, Model, Client> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<PanierReservation>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    PanierReservation u = new PanierReservation();
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

        [HttpPatch("entreprise/{entreprise:Guid}")]
        public async Task<ActionResult<PanierReservation>> ChangeEntrepriseUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid entreprise)
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

        public override async Task<ActionResult<PanierReservation>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid id)
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

        public override async Task<ActionResult<IEnumerable<PanierReservation>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x => x.Id == identity.First().Id);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<PanierReservation>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                                (x.EntrepriseId.ToString() == search) 
                                && (x.Gamme.Categorie.Name.Contains(search)),
                                x => x.Offre,
                                x => x.Gamme,
                                x => x.Offre.Marque,
                                x => x.Taille,
                                x => x.Model,
                                x => x.Offre.Categorie,
                                x => x.Offre.Style,
                                x => x.Offre.Niveau,
                                x => x.Offre.Occasionss,
                                x => x.Gamme.Marque,
                                x => x.Gamme.Categorie,
                                x => x.Gamme.Style,
                                x => x.Offre.Model,
                                x => x.Client);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("id:Guid")]
        public async Task<ActionResult<IEnumerable<Panier>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var entreprise = await _entrepriseUser.Item.GetBy(x => x.EntrepriseId == identity.First().EntrperiseId);
                    if (entreprise.Count() != 0)
                    {
                        var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id),
                                    x => x.Offre,
                                    x => x.Gamme,
                                    x => x.Offre.Marque,
                                    x => x.Taille,
                                    x => x.Model,
                                    x => x.Offre.Categorie,
                                    x => x.Offre.Style,
                                    x => x.Offre.Niveau,
                                    x => x.Offre.Occasionss,
                                    x => x.Gamme.Marque,
                                    x => x.Gamme.Categorie,
                                    x => x.Gamme.Style,
                                    x => x.Offre.Model,
                                    x => x.Client);
                        return Ok(result);
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

        public override async Task<ActionResult<IEnumerable<PanierReservation>>> AddAsync([FromBody] List<PanierReservation> values)
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
                        value.EntrepriseId = value.EntrepriseId;
                        value.UserId = identity.First().Id;
                        value.Id = Guid.NewGuid();
                        if (value.DateOfCreation == Convert.ToDateTime("0001-01-01T00:00:00"))
                            value.DateOfCreation = DateTime.Now;
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

        public override async Task<ActionResult<IEnumerable<PanierReservation>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => 
                                (x.EntrepriseId.ToString() == search) &&
                                (x.Gamme.Marque.Name.Contains(search) 
                                && x.DateOfCreation.Date >= start.Date 
                                && x.DateOfCreation.Date <= end.Date),
                                x => x.Offre,
                                x => x.Gamme,
                                x => x.Offre.Marque,
                                x => x.Taille,
                                x => x.Model,
                                x => x.Offre.Categorie,
                                x => x.Offre.Style,
                                x => x.Offre.Niveau,
                                x => x.Offre.Occasionss,
                                x => x.Gamme.Marque,
                                x => x.Gamme.Categorie,
                                x => x.Gamme.Style,
                                x => x.Offre.Model,
                                x => x.Client);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{entrepriseId:Guid}/{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PanierReservation>>> GetBy([FromRoute] Guid entrepriseId, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => 
                                x.EntrepriseId == entrepriseId &&
                                x.DateOfCreation.Date >= start.Date
                                && x.DateOfCreation.Date <= end.Date,
                                x => x.Offre,
                                x => x.Gamme,
                                x => x.Offre.Marque,
                                x => x.Taille,
                                x => x.Model,
                                x => x.Offre.Categorie,
                                x => x.Offre.Style,
                                x => x.Offre.Niveau,
                                x => x.Offre.Occasionss,
                                x => x.Gamme.Marque,
                                x => x.Gamme.Categorie,
                                x => x.Gamme.Style,
                                x => x.Offre.Model,
                                x => x.Client);

                    return Ok(result);
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
        public async Task<ActionResult<IEnumerable<Panier>>> GetBy([FromRoute] DateTime start, DateTime end)
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
    }
}
