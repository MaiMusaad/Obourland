using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObourLand.Models;
using ObourLand.Services;

namespace ObourLand.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Start GetAll users method");
            var users = await _userService.GetAll();
            _logger.LogInformation("End GetAll users method");
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            _logger.LogInformation($"Start GetById users method: {id}");
            var user = await _userService.GetById(id);
            _logger.LogInformation($"End GetById users method: {id}");
            return Ok(user);
        }

        [Authorize]
        [HttpGet("GetSupervisors")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSupervisors()
        {
            _logger.LogInformation("Start GetSupervisors method");
            var users = await _userService.GetSupervisors();
            _logger.LogInformation("End GetSupervisors method");
            return Ok(users);
        }

        [Authorize]
        [HttpGet("GetAssignedUsers/{supervisorId}")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAssignedUsers([FromRoute] int supervisorId)
        {
            _logger.LogInformation($"Start GetAssignedUsers method: {supervisorId}");
            var users = await _userService.GetAssignedUsers(supervisorId);
            _logger.LogInformation($"End GetAssignedUsers method: {supervisorId}");
            return Ok(users);
        }

        [Authorize]
        [HttpPost("AssignedUsers/{supervisorId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AssignedUsers([FromRoute] int supervisorId, List<int> userIds)
        {
            _logger.LogInformation($"Start AssignedUsers method: {supervisorId}");
            var result = await _userService.AssignedUsers(supervisorId, userIds);
            _logger.LogInformation($"End AssignedUsers method: {supervisorId}");
            if(result.IsSuccess == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("Activate/{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {
            var logger = GetCurrentUserId();
            _logger.LogInformation("Start DeactivateUser method");
            var res = await _userService.ActivateUser(id);
            _logger.LogInformation("End DeactivateUser method");
            if (res.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpPost("Deactivate/{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            _logger.LogInformation("Start DeactivateUser method");
            var res = await _userService.DeactivateUser(id);
            _logger.LogInformation("End DeactivateUser method");
            if (res.IsSuccess == false) { 
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
