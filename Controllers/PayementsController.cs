namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PayementsController : GenericController<Payement, User, Num_Payement, ModePayement>
    {
        private readonly IGenericRepositoryWrapper<Payement, User, Num_Payement, ModePayement> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public PayementsController(IGenericRepositoryWrapper<Payement, User, Num_Payement, ModePayement> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Payement>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    Payement u = new Payement();
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

        public override async Task<ActionResult<Payement>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        public override async Task<ActionResult<IEnumerable<Payement>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => x.Num_Payement);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{entrepriseId:Guid}/{search}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Payement>>> GetBy([FromRoute] Guid entrepriseId, string search)
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
                            var result = await repositoryWrapper.Item.GetByInclude(x =>
                                        (x.EntrepriseId.ToString() == search) 
                                        && (x.Nature.Contains(search) 
                                        || x.Type.Contains(search)), 
                                        x => x.Num_Payement);
                            return Ok(result);
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

        [HttpGet("id:Guid")]
        public async Task<ActionResult<IEnumerable<Payement>>> GetBy([FromRoute] Guid id)
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
                        var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id), x=> x.Num_Payement);
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

        public override async Task<ActionResult<IEnumerable<Payement>>> AddAsync([FromBody] List<Payement> values)
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
                        if (value.Date == Convert.ToDateTime("0001-01-01T00:00:00"))
                            value.Date = DateTime.Now;
                        value.EntrepriseId = value.EntrepriseId;
                        value.UserId = identity.First().Id;
                        value.Id = Guid.NewGuid();
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

        [HttpGet("{search}/{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Payement>>> GetBy([FromRoute] string search, Guid entrepriseId, DateTime start, DateTime end)
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
                            var result = await repositoryWrapper.Item.GetByInclude(x =>
                                        (x.EntrepriseId == entrepriseId)
                                        && (x.Nature.Contains(search) 
                                        || x.Type.Contains(search) 
                                        || x.Num_Operation.Contains(search)) 
                                        && x.Date.Date >= start.Date 
                                        && x.Date.Date <= end.Date, 
                                        x => x.Num_Payement, x => x.ModePayement);

                            return Ok(result);
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

        [HttpGet("{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Payement>>> GetBy([FromRoute] Guid entrepriseId, DateTime start, DateTime end)
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
                                && x.Date_Payement.Date >= start.Date 
                                && x.Date_Payement.Date <= end.Date, 
                                x => x.Num_Payement, x => x.ModePayement);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("num_operation/{num_operation}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Payement>>> GetByNum_Vente([FromRoute] string num_operation)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                                (x.Num_Operation == num_operation),
                                x => x.Num_Payement, x => x.ModePayement);

                    return Ok(result);
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
                    var entreprise = await _entrepriseUser.Item.GetBy(x => x.EntrepriseId == entrepriseId);
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
                                        && x.Date_Payement.Date >= start.Date
                                        && x.Date_Payement.Date <= end.Date, 
                                        x => x.Num_Payement, 
                                        x => x.ModePayement);

                            var charts = new List<PayementChart>();
                            foreach (var item in result)
                            {
                                charts.Add(new PayementChart()
                                {
                                    Date = item.Date_Payement,
                                    Montant = item.Montant,
                                    Type = item.Type,
                                    Nature = item.Nature,
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

    }
}
