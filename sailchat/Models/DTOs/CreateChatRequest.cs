namespace sailchat.Models.DTOs
{
    public class CreateChatRequest
    {
        public ChatType Type { get; set; }
        public List<string> ParticipantIds { get; set; }
        public List<string> AdminIds { get; set; }
    }
}
