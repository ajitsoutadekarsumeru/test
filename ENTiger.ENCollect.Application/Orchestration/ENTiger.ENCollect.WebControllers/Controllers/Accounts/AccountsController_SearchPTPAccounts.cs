using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/feedback/ptp/list")]
        [Authorize(Policy = "CanSearchMyPTPPolicy")]
        [ProducesResponseType(typeof(IEnumerable<SearchPTPAccountsDto>), 200)]
        public async Task<IActionResult> SearchPTPAccounts([FromBody] SearchPTPAccountsParams parameters)
        {
            return await RunQueryListServiceAsync<SearchPTPAccountsParams, SearchPTPAccountsDto>(parameters, _processAccountsService.SearchPTPAccounts);
        }
    }
}