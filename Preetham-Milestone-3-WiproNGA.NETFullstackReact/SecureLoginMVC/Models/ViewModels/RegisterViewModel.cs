using System.ComponentModel.DataAnnotations;

namespace SecureLoginMVC.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required, Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, Display(Name = "Role")]
        public string Role { get; set; } = "Manager"; // default
    }
}
