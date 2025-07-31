using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/dashboard")]
        [Authorize(Policy = "CanSearchAccountsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<AccountsLookupDto>), 200)]
        public async Task<IActionResult> AccountsLookup([FromBody] AccountsLookupParams parameters)
        {
            if (
                string.IsNullOrEmpty(parameters.AccountNo) 
                && string.IsNullOrEmpty(parameters.MobileNumber) 
                && string.IsNullOrEmpty(parameters.CustomerName) 
                && string.IsNullOrEmpty(parameters.CustomerID)
                && string.IsNullOrEmpty(parameters.CreditCardNumber) 
                && string.IsNullOrEmpty(parameters.PartnerLoanId)
                && string.IsNullOrEmpty(parameters.LastXDigitsOfAccountNo)
                && string.IsNullOrEmpty(parameters.LastXDigitsOfCreditCardNumber)
            )
            {
                ModelState.AddModelError("Error", "At least one of AccountNo, MobileNumber, CustomerName, CustomerID, CreditCardNumber, LastXDigitsOfAccountNo or LastXDigitsOfCreditCardNumber is required.");
                return BadRequest(ModelState);
            }
            return await RunQueryPagedServiceAsync<AccountsLookupParams, AccountsLookupDto>(parameters, _processAccountsService.AccountsLookup);
        }
    }
}