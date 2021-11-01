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
    public class EntreprisesController : GenericController<Entreprise, User, Type_Entreprise, Quartier, EntrepriseUser>
    {
        private readonly IGenericRepositoryWrapper<Entreprise, User, Type_Entreprise, Quartier, EntrepriseUser> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;

        public EntreprisesController(IGenericRepositoryWrapper<Entreprise, User, Type_Entreprise, Quartier, EntrepriseUser> wrapper,
            IConfigSettings settings, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult<Entreprise>> Delete([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var entrepriseUser = await repositoryWrapper.ItemE.GetBy(x => x.EntrepriseId == id);

                    List<Guid> list = new List<Guid>();
                    foreach (var item in entrepriseUser)
                    {
                        list.Add((Guid)(item.UserId));
                    }
                    Entreprise u = new Entreprise();
                    u.Id = id;
                    if (list.Contains(identity.First().Id))
                    {
                        foreach (var item in entrepriseUser)
                        {
                            repositoryWrapper.ItemE.Delete(item);
                            await repositoryWrapper.SaveAsync();
                        }

                        repositoryWrapper.Item.Delete(u);
                        await repositoryWrapper.SaveAsync();
                    }

                    return Ok(u);
                }
                else return NotFound("Utilisateur non identifier");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<Entreprise>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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


        public override async Task<ActionResult<IEnumerable<Entreprise>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var entrepriseUser = await repositoryWrapper.ItemE.GetBy(x => x.UserId == identity.First().Id);
                    if (entrepriseUser.Count() != 0)
                    {
                        List<Guid> list = new List<Guid>();
                        foreach (var item in entrepriseUser)
                        {
                            list.Add((Guid)(item.EntrepriseId));
                        }
                        var gro = from l in list group l by new { EntrepriseId = l } into gr select gr.Key.EntrepriseId;

                        var result = await repositoryWrapper.Item.GetByInclude(x => gro.Contains(x.Id), x => x.Type, x => x.Quartier);
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

        public override async Task<ActionResult<IEnumerable<Entreprise>>> GetBy(string search)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemE.GetBy(x => x.UserId == identity.First().Id);
                    if (result.Count() != 0)
                    {
                        List<string> list = new List<string>();
                        foreach (var item in result)
                        {
                            list.Add(item.EntrepriseId.ToString());
                        }
                        var entreprise = await repositoryWrapper.Item.GetByInclude(x => list.Contains(x.Id.ToString()), x => x.Type, x => x.Quartier);

                        return Ok(entreprise);
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

        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetBy([FromRoute] Guid id)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemE.GetBy(x => x.UserId == identity.First().Id);
                    if (result.Count() != 0)
                    {
                        var entreprise = await repositoryWrapper.Item.GetByInclude(x => x.Id == id, x => x.Type, x => x.Quartier);
                        return Ok(entreprise);
                    } else return null;
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
        public override async Task<ActionResult<IEnumerable<Entreprise>>> AddAsync([FromBody] List<Entreprise> values)
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
                        if (value.DateOfCreation == Convert.ToDateTime("0001-01-01T00:00:00"))
                            value.DateOfCreation = DateTime.Now;
                        //value.ServerTime = DateTime.Now;
                        value.Id = Guid.NewGuid();
                        value.OwnerId = identity.First().Id;
                        await repositoryWrapper.ItemA.AddAsync(value);
                        await repositoryWrapper.SaveAsync();

                        EntrepriseUser use = new EntrepriseUser();
                        use.Id = Guid.NewGuid();
                        use.EntrepriseId = value.Id;
                        use.DateOfAdd = DateTime.Now;
                        use.UserId = identity.First().Id;
                        await repositoryWrapper.ItemE.AddAsync(use);
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

        public override async Task<ActionResult<IEnumerable<Entreprise>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => x.Name.Contains(search), x => x.Type, x => x.Quartier);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("All/{search}/{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public  async Task<ActionResult<IEnumerable<Entreprise>>> GetAllBy([FromRoute] string search, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => x.Name.Contains(search), x => x.Type, x => x.Quartier);

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
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetBy([FromRoute] DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x => x.OwnerId == identity.FirstOrDefault().Id 
                    && (x.DateOfCreation.Date >= start && x.DateOfCreation.Date <= end), x => x.Type, x => x.Quartier);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All/{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetAllBy([FromRoute] DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>(x.DateOfCreation.Date >= start && x.DateOfCreation.Date <= end), x => x.Type, x => x.Quartier);

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
