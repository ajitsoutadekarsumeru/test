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
        [Route("account/contacthistory/addresses")]
        [Authorize(Policy = "CanViewAddressContactHistoryPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetAddressFromAccountContactHistoryDto>), 200)]
        public async Task<IActionResult> GetAddressFromAccountContactHistory(string accountId)
        {
            GetAddressFromAccountContactHistoryParams parameters = new GetAddressFromAccountContactHistoryParams();
            parameters.AccountId = accountId;

            return RunQueryListService<GetAddressFromAccountContactHistoryParams, GetAddressFromAccountContactHistoryDto>(
                        parameters, _processAccountsService.GetAddressFromAccountContactHistory);
        }
    }
}
