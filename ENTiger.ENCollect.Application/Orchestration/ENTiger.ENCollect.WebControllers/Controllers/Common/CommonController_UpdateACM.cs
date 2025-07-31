using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/acm/update")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateACM([FromBody] UpdateACMDto dto)
        {
            var result = RateLimit(dto, "update_acm");
            return result ?? await RunService(200, dto, _processCommonService.UpdateACM);
        }
    }
}