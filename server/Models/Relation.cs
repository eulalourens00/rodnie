using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rodnie.API.Enums;

namespace Rodnie.API.Models {
    public class Relation {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid id { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid user_id { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid group_id { get; set; }


        [Column(TypeName = "int")]
        public int status { get; set; } = (int)InviteStatusesEnum.Send;


        [Column(TypeName = "datetime")]
        public DateTime сreated_at { get; set; } = DateTime.UtcNow;


        [Column(TypeName = "datetime")]
        public DateTime updated_at { get; set; } = DateTime.UtcNow;
    }
}
