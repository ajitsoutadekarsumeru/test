using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost]
        [Route("segment/disable")]
        [Authorize(Policy = "CanDisableSegmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DisableSegments([FromBody] DisableSegmentsDto dto)
        {
            var result = RateLimit(dto, "disable_segment");
            return result ?? await RunService(200, dto, _processSegmentationService.DisableSegments);
        }
    }
}