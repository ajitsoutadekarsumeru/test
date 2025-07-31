using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/masters/import")]
        [Authorize(Policy = "CanUploadMastersPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> MastersImport([FromBody] MastersImportDto dto)
        {
            dto.CustomId = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            var result = RateLimit(dto, "upload_masters");
            return result ?? await RunService(201, dto, _processCommonService.MastersImport);
        }
    }
}