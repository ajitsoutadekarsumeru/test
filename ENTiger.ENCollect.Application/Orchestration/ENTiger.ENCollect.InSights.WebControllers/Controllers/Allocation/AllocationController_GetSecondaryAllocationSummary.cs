using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpGet()]
        [Route("secondary/allocation/summary")]
        [ProducesResponseType(typeof(IEnumerable<GetSecondaryAllocationSummaryDto>), 200)]
        public async Task<IActionResult> GetSecondaryAllocationSummary([FromQuery]GetSecondaryAllocationSummaryParams parameters)
        {
            return await RunQueryListServiceAsync<GetSecondaryAllocationSummaryParams, GetSecondaryAllocationSummaryDto>(
                        parameters, _processAllocationService.GetSecondaryAllocationSummary);
        }
    }
}
