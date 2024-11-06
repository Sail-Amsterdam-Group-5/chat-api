using Microsoft.AspNetCore.Mvc;
using sailchat.Models;
using sailchat.Models.DTOs;
using sailchat.Services;
using System.Net;

namespace sailchat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Get chat by ID
        /// </summary>
        /// <remarks>
        /// Sample chatId: chat-1
        /// </remarks>
        [HttpGet("{chatId}")]
        [ProducesResponseType(typeof(Chat), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetChat(string chatId)
        {
            var chat = _chatService.GetChat(chatId);
            return chat != null ? Ok(chat) : NotFound();
        }

        /// <summary>
        /// Create a new chat
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/chats
        ///     {
        ///         "type": "Group",
        ///         "participantIds": ["user-1", "user-2"],
        ///         "adminIds": ["user-1"]
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Chat), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateChat([FromBody] CreateChatRequest request)
        {
            var chat = new Chat
            {
                Type = request.Type,
                ParticipantIds = request.ParticipantIds,
                AdminIds = request.AdminIds
            };

            var result = _chatService.CreateChat(chat);
            return CreatedAtAction(nameof(GetChat), new { chatId = result.ChatId }, result);
        }

        /// <summary>
        /// Add user to chat
        /// </summary>
        [HttpPost("{chatId}/users/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult AddUserToChat(string chatId, string userId)
        {
            var success = _chatService.AddUserToChat(chatId, userId);
            return success ? Ok() : NotFound();
        }
    }
}
