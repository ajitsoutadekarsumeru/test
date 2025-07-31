using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ENTiger.ENCollect.AccountsModule
{
   
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [AllowAnonymous]
        [Route("account/customer/getconsentbytoken/{token}")]
        [ProducesResponseType(typeof(GetCustomerConsentByTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerConsentByToken(string token)
        {
            GetCustomerConsentByTokenParams parameters = new GetCustomerConsentByTokenParams();
            parameters.Token = token;

            return await RunQuerySingleServiceAsync<GetCustomerConsentByTokenParams, GetCustomerConsentByTokenDto>(
                        parameters, _processAccountsService.GetCustomerConsentByTokenAsync);
        }
    }
}
