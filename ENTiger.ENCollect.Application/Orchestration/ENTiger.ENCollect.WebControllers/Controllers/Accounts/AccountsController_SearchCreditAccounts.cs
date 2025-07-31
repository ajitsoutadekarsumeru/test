using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/CreditAccount")]
        [ProducesResponseType(typeof(IEnumerable<SearchCreditAccountsDto>), 200)]
        public async Task<IActionResult> SearchCreditAccounts([FromBody] SearchCreditAccountsParams parameters)
        {
            return await RunQueryListServiceAsync<SearchCreditAccountsParams, SearchCreditAccountsDto>(
                        parameters, _processAccountsService.SearchCreditAccounts);
        }
    }
}