using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("segment/all/segments")]
        [ProducesResponseType(typeof(IEnumerable<GetSegmentsDto>), 200)]
        public async Task<IActionResult> GetSegments([FromQuery] GetSegmentsParams parameters)
        {
            return await RunQueryListServiceAsync<GetSegmentsParams, GetSegmentsDto>(
                        parameters, _processSegmentationService.GetSegments);
        }
    }
}