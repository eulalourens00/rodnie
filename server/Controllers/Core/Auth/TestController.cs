using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rodnie.API.Controllers.Core.Auth {
    [ApiController]
    [Route("api/v1/core/auth/[controller]")]
    [Authorize]
    public class TestController : Controller {
        [HttpGet]
        public async Task<ActionResult> Index() {
            return Ok();
        }
    }
}
