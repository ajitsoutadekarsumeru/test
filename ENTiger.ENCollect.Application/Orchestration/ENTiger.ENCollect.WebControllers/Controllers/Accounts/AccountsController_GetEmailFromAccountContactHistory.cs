using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/contacthistory/emails")]
        [Authorize(Policy = "CanViewEmailContactHistoryPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetEmailFromAccountContactHistoryDto>), 200)]
        public async Task<IActionResult> GetEmailFromAccountContactHistory(string accountId)
        {
            GetEmailFromAccountContactHistoryParams parameters = new GetEmailFromAccountContactHistoryParams();
            parameters.AccountId = accountId;

            return RunQueryListService<GetEmailFromAccountContactHistoryParams, GetEmailFromAccountContactHistoryDto>(
                        parameters, _processAccountsService.GetEmailFromAccountContactHistory);
        }
    }
}
