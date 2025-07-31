using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPut]
        [Route("account/note/update")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateLoanAccountNote([FromBody] UpdateLoanAccountNoteDto dto)
        {
            var result = RateLimit(dto, "update_account_note");
            return result ?? await RunService(200, dto, _processAccountsService.UpdateLoanAccountNote);
        }
    }
}