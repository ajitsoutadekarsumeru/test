using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{

    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpPost()]
        [Authorize(Policy = "CanRequestSettlementPolicy")]
        [Route("status/list")]
        [ProducesResponseType(typeof(GetStatusListDto), 200)]
        public async Task<IActionResult> GetStatusList([FromQuery] GetStatusListParams parameters)
        {
            return await RunQueryListServiceAsync<GetStatusListParams, GetStatusListDto>(parameters, _processSettlementService.GetStatusList);
        }
    }
}
