using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/feedback/today/ptp/list")]
        [Authorize(Policy = "CanViewTodaysPTPPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetTodaysPTPAccountsDto>), 200)]
        public async Task<IActionResult> GetTodaysPTPAccounts()
        {
            GetTodaysPTPAccountsParams parameters = new GetTodaysPTPAccountsParams();
            return await RunQueryListServiceAsync<GetTodaysPTPAccountsParams, GetTodaysPTPAccountsDto>(parameters, _processAccountsService.GetTodaysPTPAccounts);
        }
    }
}