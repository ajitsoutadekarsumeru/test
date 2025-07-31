using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/loanAccontImport/{filename}")]
        [Authorize(Policy = "CanUploadBulkAccountsPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AccountImport(string filename)
        {
            AccountImportDto dto = new AccountImportDto() { filename = filename };
            return await RunService(201, dto, _processAccountsService.AccountImport);
        }
    }
}