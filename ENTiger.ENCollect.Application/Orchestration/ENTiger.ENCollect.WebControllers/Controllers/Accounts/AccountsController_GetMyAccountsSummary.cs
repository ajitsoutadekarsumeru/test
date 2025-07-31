using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{

    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("myaccounts/summary")]
        [Authorize(Policy = "CanViewMyAccountsSummaryPolicy")]
        [ProducesResponseType(typeof(GetMyAccountsSummaryDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyAccountsSummary([FromQuery]GetMyAccountsSummaryParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetMyAccountsSummaryParams, GetMyAccountsSummaryDto>(
                        parameters, _processAccountsService.GetMyAccountsSummary);
        }
    }
}
