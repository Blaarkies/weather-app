using Microsoft.AspNetCore.Mvc;
using WeatherApp.Domain.Models;
using WeatherApp.Services.User;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthnController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthnController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var authResponse = _userService.Authenticate(model.Username, model.Password);

            if (authResponse == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(authResponse);
        }
    }
}
