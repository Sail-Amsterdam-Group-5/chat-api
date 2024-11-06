using sailchat.Models;

namespace sailchat.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;

        public UserService()
        {
            // Initialize dummy users
            _users = new List<User>
        {
            new User
            {
                UserId = "user-1",
                NotificationEnabled = true,
                ActiveChats = new List<string> { "chat-1", "chat-2" }
            },
            new User
            {
                UserId = "user-2",
                NotificationEnabled = true,
                ActiveChats = new List<string> { "chat-1", "chat-2" }
            },
            new User
            {
                UserId = "user-3",
                NotificationEnabled = false,
                ActiveChats = new List<string> { "chat-1" }
            }
        };
        }

        public User CreateUser(User user)
        {
            user.UserId = Guid.NewGuid().ToString();
            user.NotificationEnabled = true; // Default to enabled
            user.ActiveChats = new List<string>();
            _users.Add(user);
            return user;
        }

        public User GetUser(string userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        public List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public bool UpdateNotificationSettings(string userId, bool enabled)
        {
            var user = GetUser(userId);
            if (user == null) return false;

            user.NotificationEnabled = enabled;
            return true;
        }

        public bool DeleteUser(string userId)
        {
            var user = GetUser(userId);
            if (user == null) return false;

            return _users.Remove(user);
        }

        public bool AddActiveChat(string userId, string chatId)
        {
            var user = GetUser(userId);
            if (user == null) return false;

            if (!user.ActiveChats.Contains(chatId))
            {
                user.ActiveChats.Add(chatId);
                return true;
            }
            return false;
        }

        public bool RemoveActiveChat(string userId, string chatId)
        {
            var user = GetUser(userId);
            if (user == null) return false;

            return user.ActiveChats.Remove(chatId);
        }

        public List<string> GetActiveChats(string userId)
        {
            var user = GetUser(userId);
            return user?.ActiveChats ?? new List<string>();
        }
    }
}
