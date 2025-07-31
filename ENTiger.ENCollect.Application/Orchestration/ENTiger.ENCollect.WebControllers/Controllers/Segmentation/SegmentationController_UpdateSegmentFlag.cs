using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPut]
        [Route("segment/UpdateSegmentFlag")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateSegmentFlag([FromBody] UpdateSegmentFlagDto dto)
        {
            var result = RateLimit(dto, "update_segment_flag");
            return result ?? await RunService(200, dto, _processSegmentationService.UpdateSegmentFlag);
        }
    }
}