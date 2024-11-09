using Microsoft.AspNetCore.Mvc;

namespace deflix.monolithic.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersControllerController : ApiController
    {
        private readonly ILogger<UsersControllerController> _logger;

        public UsersControllerController(ILogger<UsersControllerController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "SignUp")]
        public IActionResult SignUp()
        {
            return Ok();
        }

        [HttpPost(Name = "SignIn")]
        public IActionResult SignIn()
        {
            return Ok();
        }
    }
}
