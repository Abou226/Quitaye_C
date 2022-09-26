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
