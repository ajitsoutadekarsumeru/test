using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/customer/getconsentbyid/{id}")]
        [ProducesResponseType(typeof(GetCustomerConsentByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerConsentById(string id)
        {
            GetCustomerConsentByIdParams parameters = new GetCustomerConsentByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetCustomerConsentByIdParams, GetCustomerConsentByIdDto>(
                        parameters, _processAccountsService.GetCustomerConsentByIdAsync);
        }
    }
}
