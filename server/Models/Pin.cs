using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodnie.API.Models {
    public class Pin {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid pin_id { get; set; } = Guid.NewGuid();


        [Column(TypeName = "uniqueidentifier")]
        public Guid owner_user_id { get; set; }
    }
}
