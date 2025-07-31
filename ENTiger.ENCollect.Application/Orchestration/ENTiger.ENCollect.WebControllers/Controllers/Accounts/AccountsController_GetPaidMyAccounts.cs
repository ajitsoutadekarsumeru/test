using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("myaccount/paid/list/{skip}/{take}")]
        [Authorize(Policy = "CanViewPaidAccountsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<GetPaidMyAccountsDto>), 200)]
        public async Task<IActionResult> GetPaidMyAccountsAsync(int skip = 0, int take = 0)
        {
            GetPaidMyAccountsParams parameters = new GetPaidMyAccountsParams();
            parameters.accountsType = "paid";
            parameters.Skip = skip;
            parameters.PageSize = take;
            return await RunQueryPagedServiceAsync<GetPaidMyAccountsParams, GetPaidMyAccountsDto>(parameters, _processAccountsService.GetPaidMyAccounts);
        }
    }
}