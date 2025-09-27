using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.Models {
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; } = Guid.NewGuid();

        // ВАЖНО
        // На заметку, все аттрибуты отмеченные далее (*) - НЕ ЗАДАЮТ ОБЯЗАТЕЛЬНЫХ ПРАВИЛ
        // Это значит - Request(Rodnie.API.DTO.Requests) является тем кто ответсвеннен за валидацию этих полей
        // Аттрибуты оставлены здесь для наглядности и Request должен их дублировать
        // Для задачи обязательных правил на саму базу - надо использовать Fluent Api
        // Он будет в любом случае нужен при создании индексов

        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Username must be 3-16 characters")] // (*)
        [Column(TypeName = "nvarchar(16)")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Column(TypeName = "nvarchar(255)")]
        public string password { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")] // (*)
        [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters")]  // (*)
        [Column(TypeName = "nvarchar(20)")]
        public string? phone { get; set; }

        [Column(TypeName = "bit")]
        public bool is_verified { get; set; } = false;

        [Column(TypeName = "bit")]
        public bool is_restricted { get; set; } = false;

        [Column(TypeName = "bit")]
        public bool is_admin { get; set; } = false;

        public DateTime сreated_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }
    }
}
