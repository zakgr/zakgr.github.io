using deflix.monolithic.api.DTOs;
using deflix.monolithic.api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace deflix.monolithic.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersControllerController : ApiController
    {
        private readonly ILogger<UsersControllerController> _logger;
        private readonly IUserService _userService;

        public UsersControllerController(ILogger<UsersControllerController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("signup", Name = "SignUp")]
        public IActionResult Signup([FromBody] UserRegisterDto userDto)
        {
            if (_userService.Register(userDto))
            {
                return Ok(new { message = "Registration successful" });
            }
            return BadRequest(new { message = "Registration failed. User already exists." });
        }

        [HttpPost("signin", Name = "SignIn")]
        public IActionResult Signin([FromBody] UserLoginDto loginDto)
        {
            var user = _userService.Authenticate(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var sessionToken = _userService.CreateSessionToken(user.Id);

            return Ok(new { message = "Signin successful", sessionToken, userId = user.Id });
        }

        [HttpGet("profile")]
        public IActionResult GetProfile([FromHeader(Name = "Session-Token")] string sessionToken)
        {
            var user = _userService.GetUserBySessionToken(sessionToken);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid or missing session token" });
            }

            var profile = _userService.GetUserProfile(user.Id);
            if (profile == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(profile);
        }

        [HttpPut("profile")]
        public IActionResult UpdateProfile([FromHeader(Name = "Session-Token")] string sessionToken, [FromBody] UserProfileUpdateDto profileUpdateDto)
        {
            var user = _userService.GetUserBySessionToken(sessionToken);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid or missing session token" });
            }

            if (_userService.UpdateUserProfile(user.Id, profileUpdateDto))
            {
                return Ok(new { message = "Profile updated successfully" });
            }
            return NotFound(new { message = "User not found" });
        }

    }
}
