using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Rodnie.API.Enums;

namespace Rodnie.API.Models {
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "nvarchar(16)")]
        public string username { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string password { get; set; }

        [Column(TypeName = "nvarchar(13)")] // 10 на номер, 3 на код страны, ну вдруг
        public string phone { get; set; }

        [Column(TypeName = "bit")]
        public bool is_verified { get; set; } = false;

        [Column(TypeName = "bit")]
        public bool is_restricted { get; set; } = false;

        [Column(TypeName = "int")]
        public int role { get; set; } = (int)RolesEnum.User;

        public DateTime сreated_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
