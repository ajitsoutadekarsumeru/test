using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("myaccount/list")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchMyAccountsDto>), 200)]
        public async Task<IActionResult> SearchMyAccountsAsync([FromBody] SearchMyAccountsParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchMyAccountsParams, SearchMyAccountsDto>(
                        parameters, _processAccountsService.SearchMyAccounts);
        }
    }
}