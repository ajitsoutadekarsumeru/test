using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/MobileNoList")]
        [ProducesResponseType(typeof(IEnumerable<GetAccountContactDetailsDto>), 200)]
        public async Task<IActionResult> GetAccountContactDetails([FromBody] GetAccountContactDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetAccountContactDetailsParams, GetAccountContactDetailsDto>(
                        parameters, _processAccountsService.GetAccountContactDetails);
        }
    }
}