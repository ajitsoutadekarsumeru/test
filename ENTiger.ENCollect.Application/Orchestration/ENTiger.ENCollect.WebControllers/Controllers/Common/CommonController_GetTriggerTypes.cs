using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/trigger/types")]
        [ProducesResponseType(typeof(IEnumerable<GetTriggerTypesDto>), 200)]
        public async Task<IActionResult> GetTriggerTypes([FromQuery]GetTriggerTypesParams parameters)
        {
            return await RunQueryListServiceAsync<GetTriggerTypesParams, GetTriggerTypesDto>(
                        parameters, _processCommonService.GetTriggerTypes);
        }
    }
}
