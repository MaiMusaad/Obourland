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

        [HttpGet("GetBySupervisor/{supervisorId}")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBySupervisor([FromRoute] int supervisorId)
        {
            _logger.LogInformation($"Start GetBySupervisor method: {supervisorId}");
            var users = await _userService.GetAssignedUsers(supervisorId);
            _logger.LogInformation($"End GetBySupervisor method: {supervisorId}");
            return Ok(users);
        }

        [HttpPut("UpdateProfile")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfile(UpdateUserDto request)
        {
            _logger.LogInformation($"Start UpdateProfile method.");
            var result = await _userService.UpdateProfile(request);
            _logger.LogInformation($"End UpdateProfile method.");
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("AssignedSupervisor/{supervisorId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AssignedSupervisor([FromRoute] int supervisorId, List<int> userIds)
        {
            _logger.LogInformation($"Start AssignedSupervisor method: {supervisorId}");
            var result = await _userService.AssignedUsers(supervisorId, userIds);
            _logger.LogInformation($"End AssignedSupervisor method: {supervisorId}");
            if(result.IsSuccess == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

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
