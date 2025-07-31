using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("segment/get/autosegments")]
        [ProducesResponseType(typeof(IEnumerable<GetAutoSegmentsDto>), 200)]
        public async Task<IActionResult> GetAutoSegments([FromQuery] GetAutoSegmentsParams parameters)
        {
            return await RunQueryListServiceAsync<GetAutoSegmentsParams, GetAutoSegmentsDto>(
                        parameters, _processSegmentationService.GetAutoSegments);
        }
    }
}