using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/note/create")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddLoanAccountNote([FromBody] AddLoanAccountNoteDto dto)
        {
            var result = RateLimit(dto, "add_account_note");
            return result ?? await RunService(201, dto, _processAccountsService.AddLoanAccountNote);
        }
    }
}