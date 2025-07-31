using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpGet]
        [Route("agent/getimage")]
        [Authorize(Policy = "CanCreateAgentPolicy")]
        [Authorize(Policy = "CanUpdateAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetAgentImage(string fileName)
        {
            var dto = new GetAgentImageDto { FileName = fileName };
            var result = await RunService(200, dto, _processAgencyUsersService.GetAgentImage);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
            }
            return result;
        }
    }
}