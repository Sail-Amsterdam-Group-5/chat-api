namespace sailchat.Models
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public bool NotificationEnabled { get; set; }
        public List<string> ActiveChats { get; set; } = new();
    }
}
