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
        [Route("summary/details/aging")]
        [ProducesResponseType(typeof(FlexiPagedList<GetMySettlementDetailsByAgingDto>), 200)]
        public async Task<IActionResult> GetMySettlementDetailsByAging([FromQuery]GetMySettlementDetailsByAgingParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetMySettlementDetailsByAgingParams, GetMySettlementDetailsByAgingDto>(
                    parameters, _processSettlementService.GetMySettlementDetailsByAging);
        }
    }
}
