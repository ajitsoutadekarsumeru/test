using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("myaccount/unattempted/list/{skip}/{take}")]
        [Authorize(Policy = "CanViewUnattemptedAccountsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<GetUnAttemptedMyAccountsDto>), 200)]
        public async Task<IActionResult> GetUnAttemptedMyAccountsAsync(int skip = 0, int take = 0)
        {
            GetUnAttemptedMyAccountsParams parameters = new GetUnAttemptedMyAccountsParams();
            parameters.accountsType = "unattempted";
            parameters.Skip = skip;
            parameters.PageSize = take;

            return await RunQueryPagedServiceAsync<GetUnAttemptedMyAccountsParams, GetUnAttemptedMyAccountsDto>(parameters, _processAccountsService.GetUnAttemptedMyAccounts);
        }
    }
}