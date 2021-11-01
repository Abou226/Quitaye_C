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
    public class GatoniniSourceController : ControllerBase
    {
        private readonly IConfigSettings _settings;

        public GatoniniSourceController(IConfigSettings settings) 
        {
            _settings = settings;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetSource()
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(_settings.GatoniniSource))
                    return Ok(_settings.GatoniniSource);
                else
                 return null;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
