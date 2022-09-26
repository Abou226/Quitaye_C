namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PromotionsController : GenericController<Promotion, User, List<PromotionOffre>, 
        PromotionType>
    {
        private readonly IGenericRepositoryWrapper<Promotion, User, List<PromotionOffre>, 
            PromotionType> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IFileManager fileManager;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;
        private readonly IGenericRepositoryWrapper<PromotionOffre> promotionOffreRepos;

        public PromotionsController(IGenericRepositoryWrapper<Promotion, User, 
            List<PromotionOffre>, PromotionType> wrapper,
            IConfigSettings settings, IFileManager fileManager,
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, 
            IGenericRepositoryWrapper<PromotionOffre> promotionOffreRepos,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            this.promotionOffreRepos = promotionOffreRepos;
            _settings = settings;
            _mapper = mapper;
            this.fileManager = fileManager;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Promotion>> Delete([FromRoute] Guid id)
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
                            Promotion u = new Promotion();
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

        public override async Task<ActionResult<Promotion>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        public override async Task<ActionResult<IEnumerable<Promotion>>> GetAll()
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

        public override async Task<ActionResult<IEnumerable<Promotion>>> GetBy(string search)
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
        public async Task<ActionResult<IEnumerable<Promotion>>> GetBy([FromRoute] Guid id)
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
                            var result = await repositoryWrapper.Item.GetBy(x => (x.EntrepriseId == id));
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

        public override async Task<ActionResult<IEnumerable<Promotion>>> AddAsync([FromForm] Promotion value)
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
                        //value.UserId = identity.First().Id;
                        if (value.Image != null)
                        {
                            var result = await fileManager.Upload(_settings.AccessKey,
                                _settings.SecretKey, _settings.BucketName,
                                Amazon.RegionEndpoint.USEast1, value.Image);
                            value.Url = result.Url;
                        }
                        value.EntrepriseId = value.EntrepriseId;
                        await repositoryWrapper.ItemA.AddAsync(value);
                        await repositoryWrapper.SaveAsync();
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

        public override async Task<ActionResult<IEnumerable<Promotion>>> AddAsync([FromBody] List<Promotion> values)
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
                        //value.UserId = identity.First().Id;
                        if (value.Image != null)
                        {
                            var result = await fileManager.Upload(_settings.AccessKey,
                                _settings.SecretKey, _settings.BucketName,
                                Amazon.RegionEndpoint.USEast1, value.Image);
                            value.Url = result.Url;
                        }
                        value.Id = Guid.NewGuid();
                        var ofres = value.OffreList;
                        value.OffreList = null;
                        value.DateOfCreation = DateTime.Now;

                        value.EntrepriseId = value.EntrepriseId;
                        await repositoryWrapper.ItemA.AddAsync(value);
                        await repositoryWrapper.SaveAsync();
                        foreach (var item in ofres)
                        {
                            item.Id = Guid.NewGuid();
                            item.OffreId = value.Id;
                            await promotionOffreRepos.Item.AddAsync(item);
                            await promotionOffreRepos.SaveAsync();
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


        public override async Task<ActionResult<IEnumerable<Promotion>>> GetBy(string search, DateTime start, DateTime end)
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

        [HttpGet("{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetBy([FromRoute] DateTime start, DateTime end)
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
