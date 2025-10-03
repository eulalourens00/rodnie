using Microsoft.AspNetCore.Mvc;
using Rodnie.API.DTO.Requests.Auth;
using Rodnie.API.DTO.Responses;
using Rodnie.API.Exceptions;
using Rodnie.API.Services;

namespace Rodnie.API.Controllers.Core.Auth {
    [ApiController]
    [Route("api/v1/core/auth/[controller]")]
    public class LoginController : Controller {
        private readonly IUserService service;

        public LoginController(IUserService service) {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Login(UsernameLoginRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userResponse = await service.UsernameLoginAsync(request);
            return Ok(userResponse);
        }
    }
}
