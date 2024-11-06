using sailchat.Models;

namespace sailchat.Services
{
    public interface IMessageService
    {
        List<Message> GetChatMessages(string chatId);
        Message SendMessage(Message message);
        bool DeleteMessage(string messageId);
    }
}
