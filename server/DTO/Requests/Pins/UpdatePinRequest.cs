using System.ComponentModel.DataAnnotations;

namespace Rodnie.API.DTO.Requests.Pins
{
    public class UpdatePinRequest
    {
        [Required(ErrorMessage = "Поле title обязательно")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Введите от 3 до 50 символов")]
        public string Title { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле lat обязательно")]
        [Range(-90, 90, ErrorMessage = "Широта должна быть между -90 и 90")]
        public decimal Lat { get; set; }

        [Required(ErrorMessage = "Поле lon обязательно")]
        [Range(-180, 180, ErrorMessage = "Долгота должна быть между -180 и 180")]
        public decimal Lon { get; set; }
    }
}