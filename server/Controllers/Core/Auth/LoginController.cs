using Microsoft.AspNetCore.Mvc;
using Rodnie.API.DTO.Requests.Auth;
using Rodnie.API.DTO.Responses;
using Rodnie.API.Exceptions;
using Rodnie.API.Services;

namespace Rodnie.API.Controllers.Core.Auth {
    [ApiController]
    [Route("api/v1/core/auth/[controller]")]
    public class LoginController : Controller {
        private readonly IUserService _service;

        public LoginController(IUserService service) {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Login(UsernameLoginRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try {
                var userResponse = await _service.UsernameLoginAsync(request);
                return Ok(userResponse);
            } catch (RodnieNotFoundException ex) {
                return NotFound(new { Error = ex.Message });
            } catch (UnauthorizedAccessException ex) {
                return Unauthorized(new { Error = ex.Message });
            } catch {
                return StatusCode(500, new { Error = "Internal server error" });
            }
        }
    }
}
