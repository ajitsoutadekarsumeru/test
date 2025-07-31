using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpGet()]
        [Route("allocation/achieved/summary")]
        [ProducesResponseType(typeof(IEnumerable<GetAllocationAchievedSummaryDto>), 200)]
        public async Task<IActionResult> GetAllocationAchievedSummary([FromQuery]GetAllocationAchievedSummaryParams parameters)
        {
            return await RunQueryListServiceAsync<GetAllocationAchievedSummaryParams, GetAllocationAchievedSummaryDto>(
                        parameters, _processAllocationService.GetAllocationAchievedSummary);
        }
    }
}
