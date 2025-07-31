using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/CreditCardAccountDetails")]
        [ProducesResponseType(typeof(CreditCardAccountDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreditCardAccountDetails([FromBody] CreditCardAccountDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<CreditCardAccountDetailsParams, CreditCardAccountDetailsDto>(
                        parameters, _processAccountsService.CreditCardAccountDetails);
        }
    }
}