using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{

    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("history")]
        [ProducesResponseType(typeof(GetHistoryByIdDto), 200)]
        public async Task<IActionResult> GetHistoryById([FromQuery]GetHistoryByIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetHistoryByIdParams, GetHistoryByIdDto>(parameters, _processSettlementService.GetHistoryById);
        }

    }

    
}
