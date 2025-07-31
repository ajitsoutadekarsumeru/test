using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/Myqueue/Search")]
        [Authorize(Policy = "CanSearchBulkAccountsUploadStatusPolicy")]
        [ProducesResponseType(typeof(IEnumerable<SearchAccountImportFileStatusDto>), 200)]
        public async Task<IActionResult> SearchAccountImportFileStatus([FromBody] SearchAccountImportFileStatusParams parameters)
        {
            return await RunQueryListServiceAsync<SearchAccountImportFileStatusParams, SearchAccountImportFileStatusDto>(
                        parameters, _processAccountsService.SearchAccountImportFileStatus);
        }
    }
}