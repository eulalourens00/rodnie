using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Rodnie.API.Enums;

namespace Rodnie.API.Models {
    public class Pin {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid pin_id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "uniqueidentifier")]
        public Guid owner_user_id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string title { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string description { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal lat { get; set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal lon { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;

        [ForeignKey("owner_user_id")]
        public User id { get; set; } // я сомневаюсь, что выглядит это правильно
    }
}
