using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost]
        [Route("save/sequence")]
        [Authorize(Policy = "CanSequenceSegmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateSegmentsSequence([FromBody] UpdateSegmentsSequenceDto dto)
        {
            var result = RateLimit(dto, "save_sequence");
            return result ?? await RunService(200, dto, _processSegmentationService.UpdateSegmentsSequence);
        }
    }
}