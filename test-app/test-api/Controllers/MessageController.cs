using Microsoft.AspNetCore.Mvc;

namespace test_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World!!!";
        }
    }
}