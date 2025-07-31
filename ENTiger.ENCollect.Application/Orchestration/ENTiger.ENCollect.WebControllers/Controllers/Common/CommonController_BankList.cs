using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/get/Bank")]
        [ProducesResponseType(typeof(IEnumerable<BankListDto>), 200)]
        public async Task<IActionResult> BankList([FromQuery] BankListParams parameters)
        {
            return await RunQueryListServiceAsync<BankListParams, BankListDto>(parameters, _processCommonService.BankList);
        }
    }
}