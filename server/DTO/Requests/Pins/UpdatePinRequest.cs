using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests.Pins
{
    public class UpdatePinRequest
    {
        [Required(ErrorMessage = "���� title �����������")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "������� �� 3 �� 50 ��������")]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Required(ErrorMessage = "���� lat �����������")]
        [Range(-90, 90, ErrorMessage = "������ ������ ���� ����� -90 � 90")]
        public decimal Lat { get; set; }

        [Required(ErrorMessage = "���� lon �����������")]
        [Range(-180, 180, ErrorMessage = "������� ������ ���� ����� -180 � 180")]
        public decimal Lon { get; set; }
    }
}