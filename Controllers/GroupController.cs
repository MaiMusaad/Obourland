//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ObourLand.Models;
//using ObourLand.Services;

//namespace ObourLand.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GroupController : ControllerBase
//    {
//        private readonly ILogger<GroupController> _logger;
//        private readonly GroupService _groupService;

//        public GroupController(ILogger<GroupController> logger, GroupService groupService)
//        {
//            _logger = logger;
//            _groupService = groupService;
//        }

//        [HttpGet("Get")]
//        public async Task<IActionResult> Get()
//        {
//            _logger.LogInformation("Start GetGroup method");
//            var res = await _groupService.Get();
//            _logger.LogInformation("End GetGroup method");
//            return Ok(res);
//        }

//        [HttpPost("Create")]
//        public async Task<IActionResult> Create(GroupDto group)
//        {
//            _logger.LogInformation("Start CreateGroup method");
//             await _groupService.Create(group);
//            _logger.LogInformation("End CreateGroup method");
//            return Ok();
//        }

//        [HttpPut("Update")]
//        public async Task<IActionResult> Update(GroupDto group)
//        {
//            _logger.LogInformation("Start UpdateGroup method");
//            await _groupService.Update(group);
//            _logger.LogInformation("End UpdateGroup method");
//            return Ok();
//        }

//        [HttpPut("Activate/{id}")]
//        public async Task<IActionResult> Activate(int id)
//        {
//            _logger.LogInformation("Start DeactivateGroup method");
//            var res = await _groupService.Activate(id);
//            _logger.LogInformation("End DeactivateGroup method");
//            if (res == -1)
//            {
//                return BadRequest("Group not found");
//            }
//            return Ok(res);
//        }

//        [HttpPut("Deactivate/{id}")]
//        public async Task<IActionResult> Deactivate(int id)
//        {
//            _logger.LogInformation("Start DeactivateGroup method");
//            var res = await _groupService.Deactivate(id);
//            _logger.LogInformation("End DeactivateGroup method");
//            if (res == -1)
//            {
//                return BadRequest("Group not found");
//            }
//            return Ok(res);
//        }
//    }
//}
