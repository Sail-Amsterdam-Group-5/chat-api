using sailchat.Models;

namespace sailchat.Services
{
    public class MessageService : IMessageService
    {
        private readonly List<Message> _messages;
        public MessageService()
        {
            // Initialize dummy messages
            _messages = new List<Message>
        {
            new Message
            {
                MessageId = "msg-1",
                SenderId = "user-1",
                ChatId = "chat-1",
                Content = "Welcome to SAIL Amsterdam 2024!",
                Timestamp = DateTime.UtcNow.AddHours(-2),
                Status = MessageStatus.Read,
                Type = MessageType.Text
            },
            new Message
            {
                MessageId = "msg-2",
                SenderId = "user-2",
                ChatId = "chat-1",
                Content = "Excited to be part of the team!",
                Timestamp = DateTime.UtcNow.AddHours(-1),
                Status = MessageStatus.Delivered,
                Type = MessageType.Text
            },
            new Message
            {
                MessageId = "msg-3",
                SenderId = "user-1",
                ChatId = "chat-2",
                Content = "Meeting at dock 3 tomorrow at 9 AM",
                Timestamp = DateTime.UtcNow.AddMinutes(-30),
                Status = MessageStatus.Sent,
                Type = MessageType.Text
            }
        };
        }

        public List<Message> GetChatMessages(string chatId)
        {
            return _messages.Where(m => m.ChatId == chatId).ToList();
        }

        public Message SendMessage(Message message)
        {
            message.MessageId = Guid.NewGuid().ToString();
            message.Timestamp = DateTime.UtcNow;
            message.Status = MessageStatus.Sent;
            _messages.Add(message);
            return message;
        }

        public bool DeleteMessage(string messageId)
        {
            var message = _messages.FirstOrDefault(m => m.MessageId == messageId);
            if (message == null) return false;

            // Check 15-minute window
            if (DateTime.UtcNow - message.Timestamp > TimeSpan.FromMinutes(15))
                return false;

            return _messages.Remove(message);
        }
    }
}
