using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuitayeSourceController : ControllerBase
    {
        private readonly IConfigSettings _settings;

        public QuitayeSourceController(IConfigSettings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetSource()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(_settings.QuitayeSource))
                    return Ok(_settings.QuitayeSource);
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
