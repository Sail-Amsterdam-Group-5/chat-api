namespace sailchat.Models
{
    public class Chat
    {
        public string ChatId { get; set; } = Guid.NewGuid().ToString();
        public ChatType Type { get; set; }
        public List<string> ParticipantIds { get; set; } = new();
        public List<string> AdminIds { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }

    public enum ChatType
    {
        Individual,
        Group
    }
}
