using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/Master/UserPerformanceBand")]
        [ProducesResponseType(typeof(IEnumerable<GetUserPerformanceBandsDto>), 200)]
        public async Task<IActionResult> GetUserPerformanceBands([FromQuery] GetUserPerformanceBandsParams parameters)
        {
            return await RunQueryListServiceAsync<GetUserPerformanceBandsParams, GetUserPerformanceBandsDto>(
                        parameters, _processCommonService.GetUserPerformanceBands);
        }
    }
}