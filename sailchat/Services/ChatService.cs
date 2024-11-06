using sailchat.Models;

namespace sailchat.Services
{
    public class ChatService : IChatService
    {
        private readonly List<Chat> _chats;

        public ChatService()
        {
            // Initialize dummy chats
            _chats = new List<Chat>
        {
            new Chat
            {
                ChatId = "chat-1",
                Type = ChatType.Group,
                ParticipantIds = new List<string> { "user-1", "user-2", "user-3" },
                AdminIds = new List<string> { "user-1" },
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Chat
            {
                ChatId = "chat-2",
                Type = ChatType.Individual,
                ParticipantIds = new List<string> { "user-1", "user-2" },
                AdminIds = new List<string> { "user-1" },
                CreatedAt = DateTime.UtcNow.AddHours(-12)
            }
        };
        }

        public Chat CreateChat(Chat chat)
        {
            chat.ChatId = Guid.NewGuid().ToString();
            chat.CreatedAt = DateTime.UtcNow;
            _chats.Add(chat);
            return chat;
        }

        public Chat GetChat(string chatId)
        {
            return _chats.FirstOrDefault(c => c.ChatId == chatId);
        }

        public List<Chat> GetUserChats(string userId)
        {
            return _chats.Where(c => c.ParticipantIds.Contains(userId)).ToList();
        }

        public bool AddUserToChat(string chatId, string userId)
        {
            var chat = GetChat(chatId);
            if (chat == null) return false;

            if (!chat.ParticipantIds.Contains(userId))
            {
                chat.ParticipantIds.Add(userId);
                return true;
            }
            return false;
        }

        public bool RemoveUserFromChat(string chatId, string userId)
        {
            var chat = GetChat(chatId);
            if (chat == null) return false;

            // Can't remove user if they're the last admin
            if (chat.AdminIds.Count == 1 && chat.AdminIds.Contains(userId))
                return false;

            if (chat.ParticipantIds.Contains(userId))
            {
                chat.ParticipantIds.Remove(userId);
                chat.AdminIds.Remove(userId); // Remove from admin if they were one
                return true;
            }
            return false;
        }

        public bool AddAdminToChat(string chatId, string userId)
        {
            var chat = GetChat(chatId);
            if (chat == null) return false;

            if (!chat.ParticipantIds.Contains(userId))
                return false;

            if (!chat.AdminIds.Contains(userId))
            {
                chat.AdminIds.Add(userId);
                return true;
            }
            return false;
        }

        public bool RemoveAdminFromChat(string chatId, string userId)
        {
            var chat = GetChat(chatId);
            if (chat == null) return false;

            // Can't remove the last admin
            if (chat.AdminIds.Count <= 1)
                return false;

            return chat.AdminIds.Remove(userId);
        }

        public bool DeleteChat(string chatId)
        {
            var chat = GetChat(chatId);
            if (chat == null) return false;

            return _chats.Remove(chat);
        }

        public bool UpdateChatType(string chatId, ChatType newType)
        {
            var chat = GetChat(chatId);
            if (chat == null) return false;

            chat.Type = newType;
            return true;
        }
    }
}
