using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Rodnie.API.DTO.Requests.Pins;
using Rodnie.API.DTO.Responses.Pins;
using Rodnie.API.Services;
using System.Security.Claims;

namespace Rodnie.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class PinsController : Controller
    {
        private readonly IPinService _service;

        public PinsController(IPinService service)
        {    _service = service; }

        [HttpPost]
        public async Task<ActionResult<PinResponse>> CreatePin([FromBody] CreatePinRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            var pinResponse = await _service.CreateAsync(request, userId);
            return Ok(pinResponse);
        }

        [HttpGet]
        public async Task<ActionResult<List<PinResponse>>> GetUserPins()
        {
            var userId = GetCurrentUserId();
            var pins = await _service.GetByUserIdAsync(userId);
            return Ok(pins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PinResponse>> GetPinById(Guid id)
        {
            var userId = GetCurrentUserId();
            var pin = await _service.GetByIdAsync(id, userId);

            if (pin == null)
                return NotFound(new { error = "Пин не найден" });

            return Ok(pin);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PinResponse>> UpdatePin(Guid id, [FromBody] UpdatePinRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            var pin = await _service.UpdateAsync(id, request, userId);

            if (pin == null)
                return NotFound(new { error = "Пин не найден или у вас нет прав для редактирования" });

            return Ok(pin);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePin(Guid id)
        {
            var userId = GetCurrentUserId();
            var result = await _service.DeleteAsync(id, userId);

            if (!result)
                return NotFound(new { error = "Пин не найден или у вас нет прав для удаления" });

            return Ok(new { message = "Пин успешно удален" });
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Неверный ид пользователя");
            }
            return userId;
        }
    }
}