using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/CreditCard/dashboard/details")]
        [ProducesResponseType(typeof(GetCreditCardAccountDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCreditCardAccountDetails([FromBody] GetCreditCardAccountDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetCreditCardAccountDetailsParams, GetCreditCardAccountDetailsDto>(
                        parameters, _processAccountsService.GetCreditCardAccountDetails);
        }
    }
}