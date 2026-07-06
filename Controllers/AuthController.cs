using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObourLand.Models;
using ObourLand.Services;

namespace ObourLand.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly UserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(JwtService jwtService, UserService userService, ILogger<AuthController> logger)
        {
            _jwtService = jwtService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            _logger.LogInformation("Start Login method");
            var user = await _userService.CheckUser(request.Username, request.Password);
            if (user != null)
            {
                _logger.LogInformation("User verified");
                var token = _jwtService.GenerateToken(user);
                _logger.LogInformation("Token generated");
                return Ok(new { token });
            }

            _logger.LogWarning("User not exist");
            return BadRequest("user not found.");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            _logger.LogInformation("Start Register method");
            var res = await _userService.Create(request);
            _logger.LogInformation("End Register method");
            return Ok(res);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO request)
        {
            _logger.LogInformation("Start ChangePassword method");
            var res = await _userService.ChangePassword(request);
            _logger.LogInformation("End ChangePassword method");
            if(res.IsSuccess == false)
            {
                _logger.LogWarning("ChangePassword failed: {Message}", res.Message);
                return BadRequest(res);
            }

            return Ok(res);
        }
    }

}