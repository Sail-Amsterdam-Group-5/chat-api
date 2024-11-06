using sailchat.Models;

namespace sailchat.Services
{
    public interface IChatService
    {
        Chat CreateChat(Chat chat);
        Chat GetChat(string chatId);
        List<Chat> GetUserChats(string userId);
        bool AddUserToChat(string chatId, string userId);
        bool RemoveUserFromChat(string chatId, string userId);
        bool AddAdminToChat(string chatId, string userId);
        bool RemoveAdminFromChat(string chatId, string userId);
        bool DeleteChat(string chatId);
        bool UpdateChatType(string chatId, ChatType newType);
    }
}
