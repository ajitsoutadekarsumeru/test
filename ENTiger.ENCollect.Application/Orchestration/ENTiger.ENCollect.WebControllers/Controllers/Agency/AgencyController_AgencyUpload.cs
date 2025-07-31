using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPost]
        [Route("document/AgencyUpload")]
        [Authorize(Policy = "CanCreateAgencyPolicy")]
        [Authorize(Policy = "CanUpdateAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AgencyUpload(IFormFile file)
        {
            AgencyUploadDto dto = new AgencyUploadDto() { file = file };
            var result = RateLimit(dto, "upload_agency_document_");
            return result ?? await RunService(201, dto, _processAgencyService.AgencyUpload);
        }
    }
}