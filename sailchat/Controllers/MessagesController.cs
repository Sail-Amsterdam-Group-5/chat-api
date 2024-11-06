using Microsoft.AspNetCore.Mvc;
using sailchat.Models;
using System.Net;
using sailchat.Services;
using sailchat.Models.DTOs;

namespace sailchat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Get messages for a specific chat
        /// </summary>
        /// <remarks>
        /// Sample chatId: chat-1
        /// </remarks>
        [HttpGet("chat/{chatId}")]
        [ProducesResponseType(typeof(List<Message>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetChatMessages(string chatId)
        {
            var messages = _messageService.GetChatMessages(chatId);
            return messages.Any() ? Ok(messages) : NotFound();
        }

        /// <summary>
        /// Send a new message
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/messages
        ///     {
        ///         "senderId": "user-1",
        ///         "chatId": "chat-1",
        ///         "content": "Hello team!",
        ///         "type": "Text"
        ///     }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Message), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult SendMessage([FromBody] CreateMessageRequest request)
        {
            var message = new Message
            {
                SenderId = request.SenderId,
                ChatId = request.ChatId,
                Content = request.Content,
                Type = request.Type
            };

            var result = _messageService.SendMessage(message);
            return CreatedAtAction(nameof(GetChatMessages), new { chatId = message.ChatId }, result);
        }


        /// <summary>
        /// Delete a message (within 15-minute window)
        /// </summary>
        [HttpDelete("{messageId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteMessage(string messageId)
        {
            var success = _messageService.DeleteMessage(messageId);
            return success ? NoContent() : NotFound();
        }
    }

}
