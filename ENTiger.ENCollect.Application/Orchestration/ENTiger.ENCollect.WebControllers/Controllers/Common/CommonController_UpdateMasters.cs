using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/masters/update")]
        [Authorize(Policy = "CanDisableMasterPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateMasters([FromBody] UpdateMastersDto dto)
        {
            var result = RateLimit(dto, "update_masters");
            return result ?? await RunService(200, dto, _processCommonService.UpdateMasters);
        }
    }
}