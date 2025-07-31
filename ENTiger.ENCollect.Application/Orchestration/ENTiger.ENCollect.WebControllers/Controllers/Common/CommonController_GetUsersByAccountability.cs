using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/GetuserbyAccountType/{accountType}")]
        [ProducesResponseType(typeof(IEnumerable<GetUsersByAccountabilityDto>), 200)]
        public async Task<IActionResult> GetUsersByAccountability(string accountType)
        {
            GetUsersByAccountabilityParams parameters = new GetUsersByAccountabilityParams();
            parameters.accountType = accountType;

            return await RunQueryListServiceAsync<GetUsersByAccountabilityParams, GetUsersByAccountabilityDto>(
                        parameters, _processCommonService.GetUsersByAccountability);
        }
    }
}