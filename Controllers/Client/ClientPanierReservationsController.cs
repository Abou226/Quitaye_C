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
    public class ClientPanierReservationsController : GenericController<PanierReservation, 
        Client, Gamme, Marque, Taille, Model, Categorie, Style>
    {
        private readonly IGenericRepositoryWrapper<PanierReservation, Client, 
            Gamme, Marque, Taille, Model, Categorie, Style> repositoryWrapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUserRepository;
        private readonly IConfigSettings _settings;
        private readonly IGenericRepositoryWrapper<PanierReservation> panierRepository;
        private readonly IGenericRepositoryWrapper<Num_Vente> num_vente_repository;
        private readonly IMapper _mapper;

        public ClientPanierReservationsController(IGenericRepositoryWrapper<PanierReservation, Client, Gamme,
            Marque, Taille, Model, Categorie, Style> wrapper,
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUserRepository,
            IGenericRepositoryWrapper<PanierReservation> _panierRepository, IGenericRepositoryWrapper<Num_Vente> _num_vente_repository,
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

        public override async Task<ActionResult<PanierReservation>> PatchUpdateAsync([FromForm] JsonPatchDocument value, [FromHeader] Guid id)
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

        [HttpGet("{entrepriseId:Guid}")]
        public async Task<ActionResult<IEnumerable<PanierReservation>>> GetAll([FromRoute] Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                                (x.ClientId == identity.First().Id
                                && x.EntrepriseId == entrepriseId),
                                x => x.Gamme, 
                                x => x.Gamme.Marque, 
                                x => x.Taille,
                                x => x.Model, 
                                x => x.Gamme.Categorie, 
                                x => x.Gamme.Style);

                    return Ok(result.OrderByDescending(x => x.DateOfCreation));
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<PanierReservation>>> AddAsync([FromBody] List<PanierReservation> value)
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
                        if (item.DateOfCreation == Convert.ToDateTime("0001-01-01T00:00:00"))
                            item.DateOfCreation = DateTime.Now;
                        item.ServerTime = DateTime.Now;
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

        [HttpGet("{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PanierReservation>>> GetBy([FromRoute] DateTime start, DateTime end, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                                (x.ClientId == identity.First().Id 
                                && x.EntrepriseId == entrepriseId)
                                && x.DateOfCreation.Date >= start
                                && x.DateOfCreation.Date <= end
                                && x.EntrepriseId == entrepriseId,
                                x => x.Gamme, 
                                x => x.Gamme.Marque, 
                                x => x.Taille,
                                x => x.Model, 
                                x => x.Gamme.Categorie, 
                                x => x.Gamme.Style);
                    return Ok(result.OrderByDescending(x => x.DateOfCreation));
                        
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("interval/{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PanierReservation>>> GetForChart([FromRoute] DateTime start, DateTime end, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetBy(x =>
                        (x.EntrepriseId == entrepriseId)
                        && x.DateOfCreation.Date >= start
                        && x.DateOfCreation.Date <= end && x.Annulée == false);

                    var charts = new List<ChartData>();
                    foreach (var item in result.OrderBy(x => x.DateOfCreation).GroupBy(x => x.DateOfCreation.Date))
                    {
                        charts.Add(new ChartData()
                        {
                            Date = item.Key.Date,
                            Montant = item.Sum(x => x.Prix_Vente_Unité),
                        });
                    }
                    return Ok(charts);
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
        public async Task<ActionResult<IEnumerable<PanierReservation>>> GetBy([FromRoute] Guid entrepriseId, string search, DateTime start, DateTime end)
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
                                    && x.DateOfCreation.Date >= start 
                                    && x.DateOfCreation <= end && x.Annulée == false,
                                    x => x.Gamme, 
                                    x => x.Gamme.Marque, 
                                    x => x.Taille,
                                    x => x.Model, 
                                    x => x.Gamme.Categorie, 
                                    x => x.Gamme.Style);

                        return Ok(result.OrderByDescending(x => x.DateOfCreation));
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

        [HttpGet("livraison/{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PanierReservation>>> GetLivraison([FromRoute] DateTime start, DateTime end, Guid entrepriseId)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {

                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                                (x.EntrepriseId == entrepriseId
                                && x.UserId == identity.First().Id)
                                && x.Date_Livraison.Date >= start
                                && x.Date_Livraison.Date <= end
                                && x.Annulée == false,
                                x => x.Gamme, 
                                x => x.Gamme.Marque, 
                                x => x.Taille,
                                x => x.Model, 
                                x => x.Gamme.Categorie, 
                                x => x.Gamme.Style);

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
