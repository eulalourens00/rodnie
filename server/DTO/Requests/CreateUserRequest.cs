using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests {
    public class CreateUserRequest {
        [Required(ErrorMessage = "Поле username обязательно")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 16 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле password обязательно")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле phone обязательно")]
        [StringLength(13, ErrorMessage = "Номер телефона не может быть длинее 13 символов")]
        public string Phone { get; set; }
    }
}
