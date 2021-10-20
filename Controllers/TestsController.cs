using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
