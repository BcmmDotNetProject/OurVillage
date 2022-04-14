using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserServer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string TestMethod()
        {
            return "Hello World!.";
        }
    }
}
