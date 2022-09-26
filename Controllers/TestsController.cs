namespace Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpPost]
        public string Get([FromBody] LogInModel log)
        {
            return "Succes";
        }

        [HttpGet]
        public Test Get()
        {
            return new Test();
        }
    }
}
