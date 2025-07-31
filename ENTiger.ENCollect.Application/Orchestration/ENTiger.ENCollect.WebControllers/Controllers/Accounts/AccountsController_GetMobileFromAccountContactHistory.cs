using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/contacthistory/mobiles")]
        [Authorize(Policy = "CanViewMobileContactHistoryPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetMobileFromAccountContactHistoryDto>), 200)]
        public async Task<IActionResult> GetMobileFromAccountContactHistory(string accountId)
        {
            GetMobileFromAccountContactHistoryParams parameters = new GetMobileFromAccountContactHistoryParams();
            parameters.AccountId = accountId;

            return RunQueryListService<GetMobileFromAccountContactHistoryParams, GetMobileFromAccountContactHistoryDto>(
                        parameters, _processAccountsService.GetMobileFromAccountContactHistory);
        }
    }
}
