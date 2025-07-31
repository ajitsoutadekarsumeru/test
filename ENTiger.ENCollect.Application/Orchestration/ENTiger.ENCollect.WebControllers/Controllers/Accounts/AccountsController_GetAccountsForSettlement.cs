using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/settlement/get/list")]
        [ProducesResponseType(typeof(FlexiPagedList<GetAccountsForSettlementDto>), 200)]
        public async Task<IActionResult> GetAccountsForSettlement([FromQuery]GetAccountsForSettlementParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetAccountsForSettlementParams, GetAccountsForSettlementDto>(parameters, _processAccountsService.GetAccountsForSettlement);
        }
    }    
}
