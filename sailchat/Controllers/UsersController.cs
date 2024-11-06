using Microsoft.AspNetCore.Mvc;
using sailchat.Models;
using sailchat.Models.DTOs;
using sailchat.Services;
using System.Net;

namespace sailchat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <remarks>
        /// Sample userId: user-1
        /// </remarks>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetUser(string userId)
        {
            var user = _userService.GetUser(userId);
            return user != null ? Ok(user) : NotFound();
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/users
        ///     {
        ///         "notificationEnabled": true
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            var user = new User
            {
                NotificationEnabled = request.NotificationEnabled
            };

            var result = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { userId = result.UserId }, result);
        }

        /// <summary>
        /// Update user notification settings
        /// </summary>
        [HttpPut("{userId}/notifications")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult UpdateNotifications(string userId, [FromBody] bool enabled)
        {
            var success = _userService.UpdateNotificationSettings(userId, enabled);
            return success ? Ok() : NotFound();
        }

        /// <summary>
        /// Get user's active chats
        /// </summary>
        [HttpGet("{userId}/chats")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetUserChats(string userId)
        {
            var chats = _userService.GetActiveChats(userId);
            return Ok(chats);
        }
    }
}
