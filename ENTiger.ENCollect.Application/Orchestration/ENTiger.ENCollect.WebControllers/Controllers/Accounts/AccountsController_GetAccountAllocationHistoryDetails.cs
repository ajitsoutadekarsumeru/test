using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/allocationhistorydetails")]
        [ProducesResponseType(typeof(IEnumerable<GetAccountAllocationHistoryDetailsDto>), 200)]
        public async Task<IActionResult> GetAccountAllocationHistoryDetails([FromBody] GetAccountAllocationHistoryDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetAccountAllocationHistoryDetailsParams, GetAccountAllocationHistoryDetailsDto>(
                        parameters, _processAccountsService.GetAccountAllocationHistoryDetails);
        }
    }
}