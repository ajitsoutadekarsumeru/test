using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPut]
        [Route("account/flag/update")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateLoanAccountFlag([FromBody] UpdateLoanAccountFlagDto dto)
        {
            var result = RateLimit(dto, "update_account_flag");
            return result ?? await RunService(200, dto, _processAccountsService.UpdateLoanAccountFlag);
        }
    }
}