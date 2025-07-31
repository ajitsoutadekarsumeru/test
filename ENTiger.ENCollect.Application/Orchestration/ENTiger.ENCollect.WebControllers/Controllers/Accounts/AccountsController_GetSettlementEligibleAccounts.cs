using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{

    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/settlement/eligible/list")]
        [ProducesResponseType(typeof(FlexiPagedList<GetSettlementEligibleAccountsDto>), 200)]
        public async Task<IActionResult> GetSettlementEligibleAccounts([FromQuery]GetSettlementEligibleAccountsParams parameters)
        {
            if (
                parameters.AccountNumber == null
                && parameters.CustomerId == null
                && parameters.CurrentDPDFrom == null
                && parameters.CurrentDPDTo == null
                && parameters.NPAFlag == null
                && parameters.IsEligibleForSettlement == null
                && parameters.AccountId == null
            )
            {
                ModelState.AddModelError("Error", "At least one of Account Number, Customer Id, CurrentDPD, NPA Flag or Flagged as eligible is required.");
                return BadRequest(ModelState);
            }

            return await RunQueryPagedServiceAsync<GetSettlementEligibleAccountsParams, GetSettlementEligibleAccountsDto>(parameters, _processAccountsService.GetSettlementEligibleAccounts);
        }
    }
}
