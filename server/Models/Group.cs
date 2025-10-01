using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodnie.API.Models {
    public class Group {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid id { get; set; }


        [Column(TypeName = "nvarchar(64)")]
        public string name { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid owner_user_id { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime сreated_at { get; set; } = DateTime.UtcNow;


        [Column(TypeName = "datetime")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }   
}
