using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/accountability")]
        [ProducesResponseType(typeof(GetAccountabilityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountability([FromBody] GetAccountabilityParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetAccountabilityParams, GetAccountabilityDto>(parameters, _processCommonService.GetAccountability);
        }
    }
}