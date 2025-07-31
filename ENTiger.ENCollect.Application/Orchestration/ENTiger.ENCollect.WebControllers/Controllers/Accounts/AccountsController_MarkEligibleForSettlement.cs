using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/settlement/mark/eligible")]
        [Authorize(Policy = "CanFlagSettlementAsEligiblePolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> MarkEligibleForSettlement([FromBody]MarkEligibleForSettlementDto dto)
        {
            return await RunService(201, dto, _processAccountsService.MarkEligibleForSettlement);
        }
    }
}
