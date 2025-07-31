using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet]
        [Route("account/telecaller/todayview")]
        [Authorize(Policy = "CanViewTodaysQueuePolicy")]
        [ProducesResponseType(typeof(GetTodaysViewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodaysView()
        {
            GetTodaysViewParams parameters = new GetTodaysViewParams();

            return await RunQuerySingleServiceAsync<GetTodaysViewParams, GetTodaysViewDto>(parameters, _processAccountsService.GetTodaysView);
        }
    }
}