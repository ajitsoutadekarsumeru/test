using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        
        [HttpPost()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("summary/details")]
        [ProducesResponseType(typeof(FlexiPagedList<MySettlementSummaryDetailsDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MySettlementSummaryDetails([FromBody] MySettlementSummaryDetailsParams parameters)
        {
            return await RunQueryPagedServiceAsync<MySettlementSummaryDetailsParams, MySettlementSummaryDetailsDto>(
                        parameters, _processSettlementService.MySettlementSummaryDetails);
        }
    }
}