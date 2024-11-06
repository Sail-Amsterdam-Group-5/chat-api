using sailchat.Models;

namespace sailchat.Services
{
    public interface IUserService
    {
        User CreateUser(User user);
        User GetUser(string userId);
        List<User> GetAllUsers();
        bool UpdateNotificationSettings(string userId, bool enabled);
        bool DeleteUser(string userId);
        bool AddActiveChat(string userId, string chatId);
        bool RemoveActiveChat(string userId, string chatId);
        List<string> GetActiveChats(string userId);
    }
}
