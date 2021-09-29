﻿using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class AvariersController : GenericController<Avarier, User, Offre, Gamme, Marque, Taille, Model, Style, Categorie>
    {
        private readonly IGenericRepositoryWrapper<Avarier, User, Offre, Gamme, Marque, Taille, Model, Style, Categorie> repositoryWrapper;
        private readonly IConfigSettings _settings;
        private readonly IMapper _mapper;

        public AvariersController(IGenericRepositoryWrapper<Avarier, User, Offre, Gamme, Marque, Taille, Model, Style, Categorie> wrapper,
            IConfigSettings settings, IMapper mapper) : base(wrapper)
        {
            repositoryWrapper = wrapper;
            _settings = settings;
            _mapper = mapper;
        }

        public override async Task<ActionResult<IEnumerable<Avarier>>> GetAll()
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
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

        [HttpGet("{search}/{entrepriseId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Avarier>>> GetBy(string search, Guid entrepriseId)
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
                    && (x.Offre.Model.Name.Contains(search)
                    || x.Offre.Gamme.Marque.Name.Contains(search)),
                    x => x.Offre,
                    x => x.Offre.Gamme, x => x.Offre.Gamme.Marque,
                    x => x.Offre.Taille, x => x.Offre.Model,
                    x => x.Offre.Gamme.Style, x => x.Offre.Gamme.Categorie);

                    return Ok(result);
                }
                else return NotFound("User not indentified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public override async Task<ActionResult<Avarier>> AddAsync([FromBody] Avarier value)
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
                    //value.ServerTime = DateTime.Now;
                    value.Id = identity.First().Id;
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

        [HttpGet("{search}/{entreprise:Guid}/{start:DateTime}/{end:DateTime}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Avarier>>> GetBy([FromRoute] string search, Guid entrepriseId, DateTime start, DateTime end)
        {
            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var result = await repositoryWrapper.Item.GetByInclude(x =>
                    (x.EntrepriseId == entrepriseId)
                    && x.Date.Date >= start && x.Date <= end
                    && (x.Offre.Model.Name.Contains(search) 
                    || x.Offre.Gamme.Marque.Name.Contains(search)), x => x.Offre, 
                    x => x.Offre.Gamme, x => x.Offre.Gamme.Marque, 
                    x => x.Offre.Taille, x => x.Offre.Model, 
                    x => x.Offre.Gamme.Style, x => x.Offre.Gamme.Categorie);

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
        public async Task<ActionResult<IEnumerable<Avarier>>> GetBy([FromRoute] Guid entrepriseId, DateTime start, DateTime end)
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
                    && x.Date.Date >= start && x.Date <= end,
                    x => x.Offre,
                    x => x.Offre.Gamme, x => x.Offre.Gamme.Marque,
                    x => x.Offre.Taille, x => x.Offre.Model,
                    x => x.Offre.Gamme.Style, 
                    x => x.Offre.Gamme.Categorie);

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