using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpGet()]
        [Route("get/TrailGapDetails")]
        [ProducesResponseType(typeof(IEnumerable<GetTrailGapDownloadDetailsDto>), 200)]
        public async Task<IActionResult> GetTrailGapDownloadDetails([FromQuery] GetTrailGapDownloadDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetTrailGapDownloadDetailsParams, GetTrailGapDownloadDetailsDto>(
                        parameters, _processFeedbackService.GetTrailGapDownloadDetails);
        }
    }
}