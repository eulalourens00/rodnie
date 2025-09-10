using Microsoft.AspNetCore.Mvc;

namespace Rodnie.API.Controllers.Core.Auth {
    [ApiController]
    [Route("api/v1/core/auth/[controller]")]
    public class RegisterController : Controller {
        [HttpGet]
        public IActionResult Index() {
            return Ok("Registered");
        }
    }
}
