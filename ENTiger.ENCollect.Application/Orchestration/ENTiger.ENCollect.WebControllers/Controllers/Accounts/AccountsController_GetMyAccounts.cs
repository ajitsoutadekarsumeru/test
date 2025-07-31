using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("myaccount/list/{skip}/{take}")]
        [Authorize(Policy = "CanViewMyAccountsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<GetMyAccountsDto>), 200)]
        public async Task<IActionResult> GetMyAccountsAsync(int skip = 0, int take = 0)
        {
            GetMyAccountsParams parameters = new GetMyAccountsParams() { Skip = skip, PageSize = take };
            return await RunQueryPagedServiceAsync<GetMyAccountsParams, GetMyAccountsDto>(parameters, _processAccountsService.GetMyAccounts);
        }
    }
}