using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {

        [HttpGet()]
        [Route("mvp/get/BankAccountType")]
        [ProducesResponseType(typeof(IEnumerable<BankAccountTypesDto>), 200)]
        public async Task<IActionResult> BankAccountTypes([FromQuery] BankAccountTypesParams parameters)
        {
            return await RunQueryListServiceAsync<BankAccountTypesParams, BankAccountTypesDto>(
                        parameters, _processCommonService.BankAccountTypes);
        }
    }
}