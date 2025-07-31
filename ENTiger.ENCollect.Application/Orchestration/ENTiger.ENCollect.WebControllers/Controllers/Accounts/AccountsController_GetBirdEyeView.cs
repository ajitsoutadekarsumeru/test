using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet]
        [Route("account/telecaller/birdeyeview")]
        [Authorize(Policy = "CanViewBirdsEyePolicy")]
        [ProducesResponseType(typeof(GetBirdEyeViewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBirdEyeView()
        {
            GetBirdEyeViewParams parameters = new GetBirdEyeViewParams();

            return await RunQuerySingleServiceAsync<GetBirdEyeViewParams, GetBirdEyeViewDto>(parameters, _processAccountsService.GetBirdEyeView);
        }
    }
}