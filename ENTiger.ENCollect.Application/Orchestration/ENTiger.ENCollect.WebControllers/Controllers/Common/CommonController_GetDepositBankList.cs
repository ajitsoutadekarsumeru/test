using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/depositbankmaster")]
        [ProducesResponseType(typeof(IEnumerable<GetDepositBankListDto>), 200)]
        public async Task<IActionResult> GetDepositBankList()
        {
            GetDepositBankListParams parameters = new GetDepositBankListParams();
            return await RunQueryListServiceAsync<GetDepositBankListParams, GetDepositBankListDto>(parameters, _processCommonService.GetDepositBankList);
        }
    }
}