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
    public class VentesController : GenericController<Vente, User, Offre, Gamme, Marque, Taille, Model, Categorie>
    {
        private readonly IGenericRepositoryWrapper<Vente, User, Offre, Gamme, Marque, Taille, Model, Categorie> repositoryWrapper;
        private readonly IGenericRepositoryWrapper<EntrepriseUser> _entrepriseUserRepository;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;

        public VentesController(IGenericRepositoryWrapper<Vente, User, Offre, Gamme, Marque, Taille, Model, Categorie> wrapper, 
            IGenericRepositoryWrapper<EntrepriseUser> entrepriseUserRepository,
            IConfigSettings settings, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            _entrepriseUserRepository = entrepriseUserRepository;
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
                    || x.Offre.Gamme.Marque.Name.Contains(search) || x.Offre.Gamme.Style.Name.Contains(search)
                    || x.Offre.Gamme.Categorie.Name.Contains(search)), x => x.Offre, x => x.Offre.Gamme, 
                    x => x.Offre.Gamme.Marque, x => x.Offre.Taille, x => x.Offre.Model, x => x.Offre.Gamme.Categorie);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<Vente>> AddAsync([FromBody] Vente value)
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
                    if (value.Date == Convert.ToDateTime("0001-01-01T00:00:00"))
                        value.Date = DateTime.Now;
                    value.ServerTime = DateTime.Now;
                    value.UserId = identity.First().Id;
                    value.Id = Guid.NewGuid();
                    await repositoryWrapper.ItemA.AddAsync(value);
                    await repositoryWrapper.SaveAsync();

                    return Ok(value);
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
                        && x.Date.Date >= start && x.Date <= end,
                        x => x.Offre, x => x.Offre.Gamme,
                        x => x.Offre.Gamme.Marque, x => x.Offre.Taille,
                        x => x.Offre.Model, x => x.Offre.Gamme.Categorie);

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
                        && x.Date.Date >= start && x.Date <= end,
                        x => x.Offre, x => x.Offre.Gamme,
                        x => x.Offre.Gamme.Marque, x => x.Offre.Taille,
                        x => x.Offre.Model, x => x.Offre.Gamme.Categorie);

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
