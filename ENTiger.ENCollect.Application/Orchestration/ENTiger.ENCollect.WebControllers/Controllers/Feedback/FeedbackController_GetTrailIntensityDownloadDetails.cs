using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpGet()]
        [Route("get/TrailIntensityDetails")]
        [ProducesResponseType(typeof(IEnumerable<GetTrailIntensityDownloadDetailsDto>), 200)]
        public async Task<IActionResult> GetTrailIntensityDownloadDetails([FromQuery] GetTrailIntensityDownloadDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetTrailIntensityDownloadDetailsParams, GetTrailIntensityDownloadDetailsDto>(
                        parameters, _processFeedbackService.GetTrailIntensityDownloadDetails);
        }
    }
}