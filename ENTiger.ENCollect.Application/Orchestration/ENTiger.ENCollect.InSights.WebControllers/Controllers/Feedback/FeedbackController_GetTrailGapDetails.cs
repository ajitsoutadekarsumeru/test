using ENTiger.ENCollect.AllocationModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost()]
        [Route("trailgap/details")]
        [ProducesResponseType(typeof(FlexiPagedList<GetTrailGapDetailsDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTrailGapDetails([FromBody] GetTrailGapDetailsParams parameters)
        {
            
            return await RunQueryPagedServiceAsync<GetTrailGapDetailsParams, GetTrailGapDetailsDto>(
                        parameters, _processFeedbackService.GetTrailGapDetails);
        }
    }
}