using HomeWork_09._02._2026.Interfaces;
using HomeWork_09._02._2026.Models;
using HomeWork_09._02._2026.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork_09._02._2026.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserRequest request)
        {
            return Ok(await _userService.GetUsers(request));
        }

        [HttpGet("premium/names")]
        public async Task<IActionResult> GetPremiumUsersNames()
        {
            return Ok(await _userService.GetPremiumUsersNames());
        }

        [HttpGet("most-subs-user")]
        public async Task<IActionResult> GetUserThatHasMostSubs()
        {
            User user;

            try
            {
                user = await _userService.GetUserThatHasMostSubs();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            User createdUser;
            try
            {
                createdUser = await _userService.CreateUser(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(createdUser);
        }
    }
}