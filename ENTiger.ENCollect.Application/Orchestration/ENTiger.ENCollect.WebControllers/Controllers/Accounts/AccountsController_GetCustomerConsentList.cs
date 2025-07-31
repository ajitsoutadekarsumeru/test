using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/customer/consent/list")]
        [ProducesResponseType(typeof(IEnumerable<GetCustomerConsentListDto>), 200)]
        public async Task<IActionResult> GetCustomerConsentList([FromQuery]GetCustomerConsentListParams parameters)
        {
            return await RunQueryListServiceAsync<GetCustomerConsentListParams, GetCustomerConsentListDto>(
                        parameters, _processAccountsService.GetCustomerConsentListAsync);
        }
    }
}
