using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/offline/myaccounts")]
        [ProducesResponseType(typeof(IEnumerable<GetMyAccountsForOfflineDto>), 200)]
        public async Task<IActionResult> GetMyAccountsForOffline([FromQuery] GetMyAccountsForOfflineParams parameters)
        {
            return await RunQueryListServiceAsync<GetMyAccountsForOfflineParams, GetMyAccountsForOfflineDto>(
                        parameters, _processAccountsService.GetMyAccountsForOffline);
        }
    }
}