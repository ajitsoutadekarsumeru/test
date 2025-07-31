using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpPost()]
        [Route("segment/search")]
        [Authorize(Policy = "CanSearchSegmentPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchSegmentsDto>), 200)]
        public async Task<IActionResult> SearchSegments([FromBody] SearchSegmentsParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchSegmentsParams, SearchSegmentsDto>(parameters, _processSegmentationService.SearchSegments);
        }
    }
}