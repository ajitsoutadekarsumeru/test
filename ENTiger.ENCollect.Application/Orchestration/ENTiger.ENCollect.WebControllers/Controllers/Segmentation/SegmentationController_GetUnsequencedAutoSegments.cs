using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("segment/get/unsequencedautosegments")]
        [ProducesResponseType(typeof(IEnumerable<GetUnsequencedAutoSegmentsDto>), 200)]
        public async Task<IActionResult> GetUnsequencedAutoSegments([FromQuery] GetUnsequencedAutoSegmentsParams parameters)
        {
            return await RunQueryListServiceAsync<GetUnsequencedAutoSegmentsParams, GetUnsequencedAutoSegmentsDto>(
                        parameters, _processSegmentationService.GetUnsequencedAutoSegments);
        }
    }
}