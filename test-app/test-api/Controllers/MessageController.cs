using Microsoft.AspNetCore.Mvc;
using test_service_interfaces;

namespace test_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello World!!!";
        }

        [HttpPost]
        public void Post()
        {
            _messageService.Write("Hello World!!");
        }
    }
}