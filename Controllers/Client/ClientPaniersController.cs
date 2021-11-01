using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientPanierController : GenericController<Panier, 
        Client, Offre, Gamme, Marque, Style, Categorie, Taille, Model>
    {
        private readonly IGenericRepositoryWrapper<Panier, 
            Client, Offre, Gamme, Marque, Style, Categorie, Taille, Model> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUser;
        private readonly IGenericRepositoryWrapper<PanierReservation> panierRepository;

        public ClientPanierController(IGenericRepositoryWrapper<Panier, 
            Client, Offre, Gamme, Marque, Style, Categorie, Taille, Model> wrapper,
            IConfigSettings settings, IGenericRepositoryWrapper<EntrepriseUser> entrepriseUser, IGenericRepositoryWrapper<PanierReservation> _panierRepository, 
            IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _entrepriseUser = entrepriseUser;
            panierRepository = _panierRepository;
            _settings = settings;
            _mapper = mapper;
        }

        public override async Task<ActionResult<IEnumerable<Panier>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x => x.Id == identity.First().Id);

                    return Ok(result.OrderByDescending(x => x.Date));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Panier>>> GetBy(string search)
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
                    && (x.Offre.Gamme.Categorie.Name.Contains(search)), 
                    x => x.Offre,
                    x => x.Offre.Gamme, 
                    x => x.Offre.Gamme.Marque, 
                    x => x.Offre.Gamme.Style,
                    x => x.Offre.Gamme.Categorie, 
                    x => x.Offre.Taille, 
                    x => x.Offre.Model);

                    return Ok(result.OrderByDescending(x => x.Date));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<IEnumerable<Panier>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => 
                                        (x.EntrepriseId == id 
                                        && x.ClientId == identity.First().Id),
                                        x => x.Offre, x => x.Offre.Gamme,
                                        x => x.Offre.Gamme.Marque, 
                                        x => x.Offre.Gamme.Style,
                                        x => x.Offre.Gamme.Categorie, 
                                        x => x.Offre.Taille, 
                                        x => x.Offre.Model);

                    return Ok(result.OrderByDescending(x => x.Date));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<Panier>>> AddAsync([FromBody] List<Panier> value)
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
                    foreach (var item in value)
                    {
                        if (item.Date == Convert.ToDateTime("0001-01-01T00:00:00"))
                            item.Date = DateTime.Now;
                        item.ClientId = identity.First().Id;
                        item.Id = Guid.NewGuid();
                        await repositoryWrapper.ItemA.AddAsync(item);
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


        public override async Task<ActionResult<IEnumerable<Panier>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => (x.EntrepriseId.ToString() == search) &&
                    (x.Offre.Gamme.Marque.Name.Contains(search) 
                    && x.Date.Date >= start 
                    && x.Date.Date <= end),
                    x => x.Offre, x => x.Offre.Gamme, 
                    x => x.Offre.Gamme.Marque, 
                    x => x.Offre.Gamme.Style,
                    x => x.Offre.Gamme.Categorie, 
                    x => x.Offre.Taille, 
                    x => x.Offre.Model);

                    return Ok(result.OrderByDescending(x => x.Date));
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
        public async Task<ActionResult<IEnumerable<Panier>>> GetBy([FromRoute] DateTime start, DateTime end, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => x.EntrepriseId == entrepriseId &&
                                x.Date.Date >= start && x.Date.Date <= end,
                                x => x.Offre, x => x.Offre.Gamme,
                                x => x.Offre.Gamme.Marque, 
                                x => x.Offre.Gamme.Style,
                                x => x.Offre.Gamme.Categorie,
                                x => x.Offre.Taille, 
                                x => x.Offre.Model);

                    return Ok(result.OrderByDescending(x => x.Date));
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
