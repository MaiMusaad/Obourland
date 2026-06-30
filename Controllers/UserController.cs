using Microsoft.AspNetCore.Mvc;
using ObourLand.Services;

namespace ObourLand.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Start GetAll users method");
            var users = await _userService.GetAll();
            _logger.LogInformation("End GetAll users method");
            return Ok(users);
        }

        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            _logger.LogInformation("Start DeactivateUser method");
            var res = await _userService.ActivateUser(id);
            _logger.LogInformation("End DeactivateUser method");
            if (res == 0)
            {
                return BadRequest("user not found");
            }
            return Ok(res);
        }

        [HttpPut("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate( int id)
        {
            _logger.LogInformation("Start DeactivateUser method");
            var res = await _userService.DeactivateUser(id);
            _logger.LogInformation("End DeactivateUser method");
            if (res == 0) {
                return BadRequest("user not found");
            }
            return Ok(res);
        }
    }
}
