using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;
using Rodnie.API.Services.Groups;

namespace Rodnie.API.Controllers.Groups {
    [ApiController]
    [Route("api/v1/groups/[controller]")]
    [Authorize]
    public class CreateController : Controller {
        private readonly IGroupService service;

        public CreateController(IGroupService service) {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<GroupResponse>> Create(CreateGroupRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var response = await service.CreateAsync(request, userId);
            return Ok(response);
        }
    }
}
