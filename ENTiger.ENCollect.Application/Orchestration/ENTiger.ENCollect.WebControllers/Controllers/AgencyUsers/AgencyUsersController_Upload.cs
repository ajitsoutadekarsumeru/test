using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPost]
        [Route("document/AgentUpload")]
        [Authorize(Policy = "CanCreateAgentPolicy")]
        [Authorize(Policy = "CanUpdateAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            UploadDto dto = new UploadDto() { file = file };
            var result = RateLimit(dto, "upload_agent_document");
            return result ?? await RunService(201, dto, _processAgencyUsersService.Upload);
        }
    }
}