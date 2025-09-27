using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests {
    public class CreateUserRequest {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Username must be from 3 to 16 characters long")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [StringLength(20, ErrorMessage = "Phone must be 16 characters or less")]
        public string Phone { get; set; }
    }
}
