namespace sailchat.Models
{
    public class Message
    {
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string SenderId { get; set; }
        public string ChatId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public MessageStatus Status { get; set; }
        public MessageType Type { get; set; }
    }

    public enum MessageStatus
    {
        Sent,
        Delivered,
        Read
    }

    public enum MessageType
    {
        Text,
        Image
    }

}
