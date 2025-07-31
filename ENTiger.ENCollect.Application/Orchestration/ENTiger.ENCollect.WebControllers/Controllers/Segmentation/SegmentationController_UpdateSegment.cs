using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost]
        [Route("segment/edit")]
        [Authorize(Policy = "CanUpdateSegmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateSegment([FromBody] UpdateSegmentDto dto)
        {
            var result = RateLimit(dto, "update_segment");
            return result ?? await RunService(200, dto, _processSegmentationService.UpdateSegment);
        }
    }
}