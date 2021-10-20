using AutoMapper;
using Contracts;
using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
//using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quitaye.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : GenericController<User, Entreprise>
    {
        private readonly IGenericRepositoryWrapper<User, Entreprise> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IGenericRepositoryWrapper<RefreshToken> _refreshToken;
        private readonly IMapper _mapper;
        public UsersController(IGenericRepositoryWrapper<User, Entreprise> wrapper, 
            IConfigSettings settings, 
            IGenericRepositoryWrapper<RefreshToken> refreshToken, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            _refreshToken = refreshToken;
            _mapper = mapper;
        }

        [HttpPatch]
        public override async Task<ActionResult<User>> PatchUpdateAsync([FromBody] JsonPatchDocument value, [FromHeader] Guid id)
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

        [HttpDelete("{id}")]
        public override async Task<ActionResult<User>> Delete([FromRoute] Guid id)
        {
            try
            {
                //var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                //var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                //Equals(claim));
                //if (identity.Count() != 0)
                {
                    var values = await _refreshToken.Item.GetBy(x => x.UserId == id);
                    if (values.Count() != 0)
                    {
                        foreach (var item in values)
                        {
                            _refreshToken.Item.Delete(item);
                            await _refreshToken.SaveAsync();
                        }
                    }
                    User u = new User();
                    u.Id = id;
                    repositoryWrapper.Item.Delete(u);
                    await repositoryWrapper.SaveAsync();
                    return Ok(u);
                }
                //else return NotFound("Utilisateur non identifier");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<User>>> GetAll()
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

        [AllowAnonymous]
        [HttpGet("Email/{email}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] string email)
        {
            try
            {
                //var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                //var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                //Equals(claim));
                //if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemA.GetBy(x => (x.Email == email));
                    if (result.Count() != 0)
                    {
                        return Ok(result.First());
                    }
                    else return null;
                }
                //else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<IEnumerable<User>>> GetBy(string search)
        {
            try
            {
                //var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                //var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                //Equals(claim));
                //if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemA.GetBy(x => x.Nom.Contains(search) || x.Prenom.Contains(search));
                    
                    return Ok(result);
                }
                //else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        public override async Task<ActionResult<User>> AddAsync([FromBody] User value)
        {
            try
            {
                if (value == null)
                    return NotFound();

                //var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                //Equals(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value));

                //if (identity.Count() != 0)

                if (!string.IsNullOrWhiteSpace(value.Username))
                {
                    var u = await repositoryWrapper.ItemA.GetBy(x => x.Username == value.Username);
                    if (u.Count() != 0)
                        return BadRequest("Nom d'utilisateur deja existant, veiller choisir un nom d'utilisateur unique");
                    {
                        await Add(value);
                    }
                }
                else
                {
                    var u = await repositoryWrapper.ItemA.GetBy(x => x.Email == value.Email);
                    if (u.Count() != 0)
                        return BadRequest("Nom d'utilisateur deja existant, veiller choisir un nom d'utilisateur unique");
                    {
                        await Add(value);
                    }
                }
                
                //else return NotFound();

                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        async Task Add(User value)
        {
            if (value.Entreprise != null)
            {
                if (!string.IsNullOrWhiteSpace(value.Entreprise.Name))
                {
                    var entreprise = await repositoryWrapper.ItemB.GetBy(x => x.Name == value.Entreprise.Name);
                    if (entreprise.Count() == 0)
                    {
                        value.Entreprise.Id = Guid.NewGuid();
                        await repositoryWrapper.ItemB.AddAsync(value.Entreprise);
                        await repositoryWrapper.SaveAsync();
                    }
                    else value.Entreprise = entreprise.First();
                }
            }
            value.Id = Guid.NewGuid();
            if (value.DateOfCreation == Convert.ToDateTime("0001-01-01T00:00:00"))
                value.DateOfCreation = DateTime.Now;
            value.ServerTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(value.Password))
                value.Password = _settings.PaswordEncryption(value.Password + _settings.Key);
            await repositoryWrapper.ItemA.AddAsync(value);
            await repositoryWrapper.SaveAsync();
        }

        public override async Task<ActionResult<IEnumerable<User>>> GetBy(string search, DateTime start, DateTime end)
        {
            try
            {
                //var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                //var identity = await repositoryWrapper.ItemB.GetBy(x => x.Id.ToString().
                //Equals(claim));

                //if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.ItemA.GetBy(x => x.Nom.Contains(search) 
                    || x.Prenom.Contains(search) || x.Username.Contains(search) && 
                    (x.EntrperiseId.ToString().Equals(search) && x.DateOfCreation >= start && x.DateOfCreation <= end));
                    
                    return Ok(result);
                }
                //else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
