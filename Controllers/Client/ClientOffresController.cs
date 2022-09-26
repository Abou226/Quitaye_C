﻿namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientOffresController : GenericController<Offre, Client, Model, 
        Taille, Niveau, Marque, Style, Categorie, List<OccasionList>>
    {
        private readonly IGenericRepositoryWrapper<Offre, Client, Model, 
            Taille, Niveau, Marque, Style, Categorie, List<OccasionList>> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;

        public ClientOffresController(IGenericRepositoryWrapper<Offre, Client, Model, 
            Taille, Niveau, Marque, Style, Categorie, List<OccasionList>> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser,
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            _settings = settings;
            _mapper = mapper;
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
                    var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId == id),
                                x => x.Model,
                                x => x.Taille,
                                x => x.Niveau,
                                x => x.Marque,
                                x => x.Style,
                                x => x.Categorie, 
                                x => x.Occasionss);
                                return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{search}/{id:Guid}")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetBy([FromRoute] string search, Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => 
                    (x.EntrepriseId == id && (x.Categorie.Name.Contains(search) 
                    || x.Style.Name.Contains(search) 
                    || x.Taille.Name.Contains(search))),
                                x => x.Model,
                                x => x.Taille,
                                x => x.Niveau,
                                x => x.Marque,
                                x => x.Style,
                                x => x.Categorie,
                                x => x.Occasionss);
                    return Ok(result);
                }
                else return NotFound("User not indentified");
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
                    var result = await repositoryWrapper.Item.GetByInclude(x => 
                                (x.EntrepriseId.ToString() == search)
                                && (x.Marque.Name.Contains(search)),
                                x => x.Model,
                                x => x.Taille,
                                x => x.Niveau,
                                x => x.Marque,
                                x => x.Style,
                                x => x.Categorie, 
                                x => x.Occasionss);

                    return Ok(result);
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
                                    x => x.Marque,
                                    x => x.Style,
                                    x => x.Categorie,
                                    x => x.Occasionss);

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
                                x => x.Marque,
                                x => x.Style,
                                x => x.Categorie,
                                x => x.Occasionss);

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