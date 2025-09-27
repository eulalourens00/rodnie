using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests.Auth {
    public class UsernameLoginRequest {
        [Required(ErrorMessage = "Поле username обязательно")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 16 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле password обязательно")]
        public string Password { get; set; }
    }
}
