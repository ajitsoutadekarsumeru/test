using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/{accountno}")]
        [ProducesResponseType(typeof(GetAccountByAccountNoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountByAccountNo(string accountno)
        {
            GetAccountByAccountNoParams parameters = new GetAccountByAccountNoParams();
            parameters.accountno = accountno;

            return await RunQuerySingleServiceAsync<GetAccountByAccountNoParams, GetAccountByAccountNoDto>(
                        parameters, _processAccountsService.GetAccountByAccountNo);
        }
    }
}