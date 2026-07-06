using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObourLand.Models;
using ObourLand.Services;

namespace ObourLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : BaseController
    {
        private readonly ILogger<GroupController> _logger;
        private readonly GroupService _groupService;

        public GroupController(ILogger<GroupController> logger, GroupService groupService)
        {
            _logger = logger;
            _groupService = groupService;
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(List<GroupDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<GroupDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Start GetGroup method");
            var res = await _groupService.Get();
            _logger.LogInformation("End GetGroup method");
            return Ok(res);
        }

        [HttpGet("GetUsersByGroup/{groupId}")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsersByGroup([FromRoute] int groupId)
        {
            _logger.LogInformation("Start GetUsersByGroup method");
            var res = await _groupService.GetUsersByGroup(groupId);
            _logger.LogInformation("End GetUsersByGroup method");
            return Ok(res);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(GroupDto group)
        {
            _logger.LogInformation("Start CreateGroup method");
            var res = await _groupService.Create(group);
            _logger.LogInformation("End CreateGroup method");
            if (res.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(GroupDto group)
        {
            _logger.LogInformation("Start UpdateGroup method");
            var res = await _groupService.Update(group);
            _logger.LogInformation("End UpdateGroup method");
            if (res.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost("AssignedUsersToGroup/{groupId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AssignedUsersToGroup([FromRoute] int groupId, List<int> userIds)
        {
            _logger.LogInformation("Start AssignedUsersToGroup method");
            var res = await _groupService.AssignedUsersToGroup(groupId, userIds);
            _logger.LogInformation("End AssignedUsersToGroup method");
            if (res.IsSuccess == false) { 
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpPut("Activate/{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Activate(int id)
        {
            _logger.LogInformation("Start ActivateGroup method");
            var res = await _groupService.Activate(id);
            _logger.LogInformation("End ActivateGroup method");
            if (res.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPut("Deactivate/{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Deactivate(int id)
        {
            _logger.LogInformation("Start DeactivateGroup method");
            var res = await _groupService.Deactivate(id);
            _logger.LogInformation("End DeactivateGroup method");
            if (res.IsSuccess == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
