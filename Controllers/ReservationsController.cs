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
    public class ReservationsController : GenericController<Reservation, User, Gamme, Marque, Taille, Model, Categorie, Style, Client, Marque>
    {
        private readonly IGenericRepositoryWrapper<Reservation, User, Gamme, Marque, Taille, Model, Categorie, Style, Client, Marque> repositoryWrapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUserRepository;
        private readonly IConfigSettings _settings;
        private readonly IGenericRepositoryWrapper<PanierReservation> panierRepository;
        private readonly IGenericRepositoryWrapper<Num_Vente> num_vente_repository;
        private readonly IMapper _mapper;

        public ReservationsController(IGenericRepositoryWrapper<Reservation, User, Gamme,
            Marque, Taille, Model, Categorie, Style, Client, Marque> wrapper,
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
                    Reservation u = new Reservation();
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

        public override async Task<ActionResult<Reservation>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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
                    x => x.Gamme, 
                    x => x.Gamme.Marque, 
                    x => x.Taille, 
                    x => x.Model, 
                    x => x.Gamme.Categorie, 
                    x => x.Gamme.Style, 
                    x => x.Client,
                    x => x.Marque);

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
                    var entreprise = await _entrepriseUserRepository.Item.GetBy(x => x.EntrepriseId == value.First().EntrepriseId);
                    if (entreprise.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entreprise)
                        {
                            list.Add((Guid)item.UserId);
                        }

                        if (list.Contains(identity.First().Id))
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
                            var num_vente = await num_vente_repository.Item.AddAsync(numss);
                            foreach (var item in value)
                            {
                                if (item.DateOfCreation == Convert.ToDateTime("0001-01-01T00:00:00"))
                                    item.DateOfCreation = DateTime.Now;
                                item.ServerTime = DateTime.Now;
                                item.UserId = identity.First().Id;
                                item.Id = Guid.NewGuid();
                                item.NumVenteId = num_vente.Id;
                                await repositoryWrapper.ItemA.AddAsync(item);
                                await repositoryWrapper.SaveAsync();
                                var pan = await panierRepository.Item.GetBy(x => x.Id == item.PanierId);
                                panierRepository.Item.Delete(pan.First());
                                await panierRepository.SaveAsync();
                            }
                            return Ok(value);
                        }
                        else return NotFound("Non membre cette entreprise");
                    }
                    else return NotFound("Non membre de cette entreprise");
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
                                        x => x.Gamme,
                                        x => x.Gamme.Marque, 
                                        x => x.Taille,
                                        x => x.Model, 
                                        x => x.Gamme.Categorie,
                                        x => x.Gamme.Style,
                                        x => x.Client,
                                        x => x.Marque);
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

        [HttpGet("interval/{start:DateTime}/{end:DateTime}/{entrepriseId:Guid}")]
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
                                && x.DateOfCreation.Date >= start.Date.AddDays(-6) 
                                && x.DateOfCreation.Date <= end.Date && x.Annulée == false);

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
                                x => x.Gamme,
                                x => x.Gamme.Marque,
                                x => x.Taille,
                                x => x.Model,
                                x => x.Gamme.Categorie,
                                x => x.Gamme.Style,
                                x => x.Client, 
                                x => x.Marque);

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
