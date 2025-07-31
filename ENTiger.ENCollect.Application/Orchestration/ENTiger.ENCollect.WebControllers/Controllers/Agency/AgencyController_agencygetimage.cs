using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet]
        [Route("agency/getimage")]
        [Authorize(Policy = "CanCreateAgencyPolicy")]
        [Authorize(Policy = "CanUpdateAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> agencygetimage(string fileName)
        {
            agencygetimageDto dto = new agencygetimageDto() { FileName = fileName };
            var result = await RunService(200, dto, _processAgencyService.agencygetimage);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileDefaultPathAsync(dto.FileName);
            }
            return result;
        }
    }
}