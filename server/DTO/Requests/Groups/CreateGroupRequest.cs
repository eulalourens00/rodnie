using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests.Groups {
    public class CreateGroupRequest {
        [Required(ErrorMessage = "Поле name обязательно")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Название группы должно быть от 3 до 64 символов")]
        public string name { get; set; }
    }
}
