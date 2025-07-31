using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost]
        [Route("segment/enable")]
        [Authorize(Policy = "CanEnableSegmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> EnableSegments([FromBody] EnableSegmentsDto dto)
        {
            var result = RateLimit(dto, "enable_segment");
            return result ?? await RunService(200, dto, _processSegmentationService.EnableSegments);
        }
    }
}