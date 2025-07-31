using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/search")]
        [Authorize(Policy = "CanSearchAccountsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchAccountsByFilterDto>), 200)]
        public async Task<IActionResult> SearchAccountsByFilter([FromBody] SearchAccountsByFilterParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchAccountsByFilterParams, SearchAccountsByFilterDto>(
                        parameters, _processAccountsService.SearchAccountsByFilter);
        }
    }
}