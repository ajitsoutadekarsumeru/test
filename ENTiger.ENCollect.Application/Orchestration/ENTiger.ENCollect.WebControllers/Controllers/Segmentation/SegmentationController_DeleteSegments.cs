using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost()]
        [Route("segment/delete")]
        [Authorize(Policy = "CanDeleteSegmentPolicy")]
        public async Task<IActionResult> DeleteSegments([FromBody] DeleteSegmentsDto dto)
        {
            var result = RateLimit(dto, "delete_segment");
            return result ?? await RunService(200, dto, _processSegmentationService.DeleteSegments);
        }
    }
}