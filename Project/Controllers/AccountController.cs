using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterDto registerData)
        {
            _userService.Register(registerData);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginData)
        {
            var receivedToken =  _userService.Validate(loginData);
            return Ok(new {token = receivedToken});
        }
    }
}