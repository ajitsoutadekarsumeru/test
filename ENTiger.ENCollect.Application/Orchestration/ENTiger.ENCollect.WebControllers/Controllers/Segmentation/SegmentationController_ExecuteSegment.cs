using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost]
        [Route("segment/executesegment")]
        [Authorize(Policy = "CanExecuteSegmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ExecuteSegment([FromBody] ExecuteSegmentDto dto)
        {
            var result = RateLimit(dto, "execute_segment_");
            return result ?? await RunService(200, dto, _processSegmentationService.ExecuteSegment);
        }
    }
}