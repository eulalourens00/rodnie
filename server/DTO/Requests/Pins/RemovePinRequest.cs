using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests
{
    public class RemovePinRequest
    {
        [Required(ErrorMessage = "ID ���� ����������")]
        public Guid id { get; set; }
    }
}