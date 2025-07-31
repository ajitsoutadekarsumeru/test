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
        [Route("currentqueue")]
        [ProducesResponseType(typeof(FlexiPagedList<GetCurrentQueueByIdDto>), 200)]
        public async Task<IActionResult> GetCurrentQueueById([FromQuery]GetCurrentQueueByIdParams parameters)
        {
            return RunQueryPagedService<GetCurrentQueueByIdParams, GetCurrentQueueByIdDto>(parameters, _processSettlementService.GetCurrentQueueById);
        }

    }

    
}
