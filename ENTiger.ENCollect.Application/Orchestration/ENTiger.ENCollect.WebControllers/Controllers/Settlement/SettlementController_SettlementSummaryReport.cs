using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        
        [HttpPost()]
        [Authorize(Policy = "CanRequestSettlementPolicy")]
        [Route("report")]
        [ProducesResponseType(typeof(FlexiPagedList<SettlementReportDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SettlementReport([FromBody] SettlementReportParams parameters)
        {
            return await RunQueryPagedServiceAsync<SettlementReportParams, SettlementReportDto>(
                        parameters, _processSettlementService.SettlementReport);
        }
    }
}