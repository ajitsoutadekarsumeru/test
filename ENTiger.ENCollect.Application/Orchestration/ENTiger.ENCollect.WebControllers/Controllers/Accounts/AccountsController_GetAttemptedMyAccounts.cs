using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("myaccount/attempted/list/{skip}/{take}")]
        [Authorize(Policy = "CanViewAttemptedAccountsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<GetAttemptedMyAccountsDto>), 200)]
        public async Task<IActionResult> GetAttemptedMyAccountsAsync(int skip = 0, int take = 0)
        {
            GetAttemptedMyAccountsParams parameters = new GetAttemptedMyAccountsParams();
            parameters.accountsType = "attempted";
            parameters.Skip = skip;
            parameters.PageSize = take;

            return await RunQueryPagedServiceAsync<GetAttemptedMyAccountsParams, GetAttemptedMyAccountsDto>(parameters, _processAccountsService.GetAttemptedMyAccounts);
        }
    }
}