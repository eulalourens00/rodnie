using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodnie.API.Models {
    public class Relation {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid id { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid relation_user_id { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid relation_group_id { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime сreated_at { get; set; } = DateTime.UtcNow;


        [Column(TypeName = "datetime")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
