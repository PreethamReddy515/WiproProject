namespace RealTimeChatApp.Backend.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = default!;
        public string? ReceiverId { get; set; } // for private chat
        public int? GroupId { get; set; } // for group chat
        public string Content { get; set; } = string.Empty;
        public string? AttachmentUrl { get; set; }
        public DateTime SentAtUtc { get; set; } = DateTime.UtcNow;
    }
}
