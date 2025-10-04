using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests.Groups {
    public class CreateRelationRequest {
        [Required(ErrorMessage = "Поле user_id обязательно")]
        public Guid user_id { get; set; }

        [Required(ErrorMessage = "Поле group_id обязательно")]
        public Guid group_id { get; set; }
    }
}
