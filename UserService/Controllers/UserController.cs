using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("getUsers")]
        public IActionResult GetUsers()
        {
            _logger.LogDebug("Started: Inside UserController - getUsers");
            var users = _userService.GetUsers();
            _logger.LogDebug("Completed: Inside UserController - getUsers");
            return Ok(users);
        }

        [Authorize]
        [HttpPost]
        [Route("addUser")]
        public IActionResult AddUser(User user)
        {
            _logger.LogDebug("Started: Inside UserController - AddUser");
            _userService.AddUser(user);
            _logger.LogDebug("Completed: Inside UserController - AddUser");
            return Ok("Success");
        }
    }
}
