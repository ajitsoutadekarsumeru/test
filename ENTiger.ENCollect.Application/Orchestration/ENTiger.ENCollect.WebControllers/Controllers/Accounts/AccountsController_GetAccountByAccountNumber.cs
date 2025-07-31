using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account")]
        [ProducesResponseType(typeof(GetAccountByAccountNumberDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountByAccountNumber([FromBody] GetAccountByAccountNumberParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetAccountByAccountNumberParams, GetAccountByAccountNumberDto>(parameters, _processAccountsService.GetAccountByAccountNumber);
        }
    }
}