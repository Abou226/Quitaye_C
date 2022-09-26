namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OffresController : GenericController<Offre, User, 
        Model, Taille, Niveau, 
        List<OccasionList>, Marque, 
        Style, Categorie>
    {
        private readonly IGenericRepositoryWrapper<Offre, User, 
            Model, Taille, Niveau, List<OccasionList>, 
            Marque, Style, Categorie> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;
        private readonly IGenericRepositoryWrapper<OccasionList> occasionsRepository;

        public OffresController(IGenericRepositoryWrapper<Offre, User, 
            Model, Taille, Niveau, List<OccasionList>, 
            Marque, Style, Categorie> wrapper, 
            IGenericRepositoryWrapper<OccasionList> _occasionsRepository,
            IConfigSettings settings, 
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, 
            IFileManager fileManager,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _fileManager = fileManager;
            occasionsRepository = _occasionsRepository;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Offre>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    Offre u = new Offre();
                    u.Id = id;
                    var items = await occasionsRepository.Item.GetBy(x => x.OffreId == id);
                    if(items.Count() != 0)
                    {
                        foreach (var item in items)
                        {
                            occasionsRepository.Item.Delete(item);
                            await occasionsRepository.SaveAsync();
                        }
                    }
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
        public async Task<ActionResult<Offre>> ChangeEntrepriseUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid entreprise)
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

        public override async Task<ActionResult<Offre>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromRoute] Guid id)
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

        public override async Task<ActionResult<IEnumerable<Offre>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<Offre>>> GetBy(string search)
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
                                && (x.Categorie.Name.Contains(search)),
                                x => x.Model, 
                                x => x.Taille, 
                                x => x.Niveau, 
                                x => x.Occasionss, 
                                x => x.Marque, 
                                x => x.Style, 
                                x => x.Categorie);

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
        public async Task<ActionResult<IEnumerable<Offre>>> GetBy([FromRoute] Guid id)
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
                            var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id),
                                        x => x.Model,
                                        x => x.Taille,
                                        x => x.Niveau,
                                        x => x.Occasionss,
                                        x => x.Marque,
                                        x => x.Style,
                                        x => x.Categorie);
                            return Ok(result);
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

        [HttpGet("by_style/{id:Guid}/{entrepriseId:Guid}")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetByStyle([FromRoute] Guid id, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                                (x.StyleId.Equals(id)),
                                x => x.Model,
                                x => x.Taille,
                                x => x.Niveau,
                                x => x.Occasionss,
                                x => x.Marque,
                                x => x.Style,
                                x => x.Categorie);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("single/{id:Guid}/{entrepriseId:Guid}")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetBy([FromRoute] Guid id, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                                (x.Id.Equals(id)),
                                x => x.Model,
                                x => x.Taille,
                                x => x.Niveau,
                                x => x.Occasionss,
                                x => x.Marque,
                                x => x.Style,
                                x => x.Categorie);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        public override async Task<ActionResult<IEnumerable<Offre>>> AddAsync([FromBody] List<Offre> values)
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
                        var offrese = await repositoryWrapper.Item.GetBy(x => x.StyleId == value.StyleId
                        && x.MarqueId == value.MarqueId
                        && x.CategorieId == value.CategorieId
                        && x.NiveauId == value.NiveauId
                        && x.ModelId == value.ModelId);
                        if (offrese.Count() == 0)
                        {
                            if (value.Image != null)
                            {
                                var result = await _fileManager.Upload(_settings.AccessKey,
                                    _settings.SecretKey, _settings.BucketName,
                                    Amazon.RegionEndpoint.USEast1, value.Image);
                                value.Url = result.Url;
                            }

                            value.Id = Guid.NewGuid();
                            var occase = value.Occasionss;
                            value.Occasionss = null;
                            value.UserId = identity.First().Id;
                            value.EntrepriseId = value.EntrepriseId;
                            await repositoryWrapper.ItemA.AddAsync(value);
                            await repositoryWrapper.SaveAsync();
                            foreach (var item in occase)
                            {
                                item.Id = Guid.NewGuid();
                                item.EntrepriseId = value.EntrepriseId;
                                item.OffreId = value.Id;
                                item.UserId = identity.First().Id;
                                await occasionsRepository.Item.AddAsync(item);
                                await occasionsRepository.SaveAsync();
                            }
                            value.Occasionss = occase;
                        }
                        else return Ok(offrese);
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

        public override async Task<ActionResult<IEnumerable<Offre>>> AddAsync([FromForm] Offre value)
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
                    //foreach (var value in values)
                    {
                        if (value.Image != null)
                        {
                            var result = await _fileManager.Upload(_settings.AccessKey,
                                _settings.SecretKey, _settings.BucketName,
                                Amazon.RegionEndpoint.USEast1, value.Image);
                            value.Url = result.Url;
                        }

                        value.Id = Guid.NewGuid();
                        var occase = value.Occasionss;
                        value.Occasionss = null;
                        value.UserId = identity.First().Id;
                        value.EntrepriseId = value.EntrepriseId;
                        await repositoryWrapper.ItemA.AddAsync(value);
                        await repositoryWrapper.SaveAsync();
                        foreach (var item in occase)
                        {
                            item.Id = Guid.NewGuid();
                            item.EntrepriseId = value.EntrepriseId;
                            item.UserId = identity.First().Id;
                            item.OffreId = value.Id;
                            await occasionsRepository.Item.AddAsync(item);
                            await occasionsRepository.SaveAsync();
                        }
                        value.Occasionss = occase;
                    }
                    return Ok(value);
                }
                else return NotFound("User not identified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Offre>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId.ToString() == search) 
                                && (x.Marque.Name.Contains(search)),
                                x => x.Model,
                                x => x.Taille,
                                x => x.Niveau,
                                x => x.Occasionss,
                                x => x.Marque,
                                x => x.Style,
                                x => x.Categorie);

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
        public async Task<ActionResult<IEnumerable<Offre>>> GetBy([FromRoute] DateTime start, DateTime end)
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
