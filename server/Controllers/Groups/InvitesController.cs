using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rodnie.API.DTO.Requests.Groups;
using Rodnie.API.DTO.Responses.Groups;

namespace Rodnie.API.Controllers.Groups {
    [ApiController]
    [Route("api/v1/groups/[controller]")]
    [Authorize]
    public class InvitesController : Controller {
        [HttpPost]
        public async Task<ActionResult<RelationResponse>> CreateInvite(CreateRelationRequest request) {
            return View();
        }
    }
}
