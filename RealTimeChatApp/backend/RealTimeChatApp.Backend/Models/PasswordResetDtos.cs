namespace RealTimeChatApp.Backend.Models
{
    public class PasswordResetDtos
    {
        public record PasswordResetRequestDto(string Email);
        public record PasswordResetConfirmDto(string Email, string Token, string NewPassword);
    }
}
