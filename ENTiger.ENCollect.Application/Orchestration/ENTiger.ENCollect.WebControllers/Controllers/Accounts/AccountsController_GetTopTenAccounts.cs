using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/top/ten/list")]
        [Authorize(Policy = "CanViewTopTenAccountsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetTopTenAccountsDto>), 200)]
        public async Task<IActionResult> GetTopTenAccounts([FromQuery] GetTopTenAccountsParams parameters)
        {
            return await RunQueryListServiceAsync<GetTopTenAccountsParams, GetTopTenAccountsDto>(
                        parameters, _processAccountsService.GetTopTenAccounts);
        }
    }
}