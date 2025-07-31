using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/labels")]
        [ProducesResponseType(typeof(IEnumerable<GetAccountLabelsDto>), 200)]
        public async Task<IActionResult> GetAccountLabels([FromQuery] GetAccountLabelsParams parameters)
        {
            return await RunQueryListServiceAsync<GetAccountLabelsParams, GetAccountLabelsDto>(
                        parameters, _processAccountsService.GetAccountLabels);
        }
    }
}