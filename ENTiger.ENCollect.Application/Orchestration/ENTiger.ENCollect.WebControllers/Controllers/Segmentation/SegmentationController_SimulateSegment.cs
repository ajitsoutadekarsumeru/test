using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost()]
        [Route("segment/simulate")]
        [Authorize(Policy = "CanCompareSegmentPolicy")]
        [ProducesResponseType(typeof(IEnumerable<SimulateSegmentDto>), 200)]
        public async Task<IActionResult> SimulateSegment([FromBody] SimulateSegmentParams parameters)
        {
            return await RunQueryListServiceAsync<SimulateSegmentParams, SimulateSegmentDto>(
                        parameters, _processSegmentationService.SimulateSegment);
        }
    }
}