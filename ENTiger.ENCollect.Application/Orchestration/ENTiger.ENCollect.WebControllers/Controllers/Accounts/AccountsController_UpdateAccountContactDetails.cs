using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/UpdateMobileNoList")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateAccountContactDetails([FromBody] UpdateAccountContactDetailsDto dto)
        {
            var result = RateLimit(dto, "update_account_mobileNo");
            return result ?? await RunService(200, dto, _processAccountsService.UpdateAccountContactDetails);
        }
    }
}