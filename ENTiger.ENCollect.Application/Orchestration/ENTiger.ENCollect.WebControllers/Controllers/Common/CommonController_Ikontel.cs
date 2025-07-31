using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/ikontel")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> Ikontel([FromBody] IkontelDto dto)
        {
            var result = RateLimit(dto, "ikontel");
            return result ?? await RunService(201, dto, _processCommonService.Ikontel);
        }
    }
}