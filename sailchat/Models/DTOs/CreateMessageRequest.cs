namespace sailchat.Models.DTOs
{
    public class CreateMessageRequest
    {
        public string SenderId { get; set; }
        public string ChatId { get; set; }
        public string Content { get; set; }
        public MessageType Type { get; set; } = MessageType.Text;
    }
}
