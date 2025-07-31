using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/telecaller/dashboard/details")]
        [ProducesResponseType(typeof(GetTeleCallerAccountDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeleCallerAccountDetails([FromBody] GetTeleCallerAccountDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetTeleCallerAccountDetailsParams, GetTeleCallerAccountDetailsDto>(
                parameters, _processAccountsService.GetTeleCallerAccountDetails);
        }
    }
}