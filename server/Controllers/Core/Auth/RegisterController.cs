using Microsoft.AspNetCore.Mvc;
using Rodnie.API.DTO.Requests;
using Rodnie.API.DTO.Responses;
using Rodnie.API.Services;

namespace Rodnie.API.Controllers.Core.Auth {
    [ApiController]
    [Route("api/v1/core/auth/[controller]")]
    public class RegisterController : Controller {
        private readonly IUserService _service;

        public RegisterController(IUserService service) {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Register(CreateUserRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try {
                var userResponse = await _service.CreateAsync(request);
                return Ok(userResponse);
            } catch (Exception ex) {
                return Conflict(new { Error = ex.Message });
            }
        }
    }
}
