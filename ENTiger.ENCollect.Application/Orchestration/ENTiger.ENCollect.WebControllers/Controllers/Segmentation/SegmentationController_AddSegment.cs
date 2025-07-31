using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost]
        [Route("segment/add")]
        [Authorize(Policy = "CanCreateSegmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddSegment([FromBody] AddSegmentDto dto)
        {
            var result = RateLimit(dto, "add_segment");
            return result ?? await RunService(201, dto, _processSegmentationService.AddSegment);
        }
    }
}