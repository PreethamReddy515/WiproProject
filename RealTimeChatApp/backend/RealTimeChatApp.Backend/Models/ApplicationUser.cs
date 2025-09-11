using Microsoft.AspNetCore.Identity;

namespace RealTimeChatApp.Backend.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? DisplayName { get; set; }
        public string Status { get; set; } = "Available"; // Available | Busy |Offline
        public DateTime LastSeenUtc { get; set; } = DateTime.UtcNow;
    }
}
