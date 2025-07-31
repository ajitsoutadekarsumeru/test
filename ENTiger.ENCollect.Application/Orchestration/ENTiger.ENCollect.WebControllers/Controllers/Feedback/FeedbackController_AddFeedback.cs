using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost]
        [Route("account/feedback/add")]
        [Authorize(Policy = "CanAddTrailPolicy")]
        [Authorize(Policy = "CanAddPTPPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackDto dto)
        {
            var result = RateLimit(dto, "add_feedback");
            return result ?? await RunService(201, dto, _processFeedbackService.AddFeedback);
        }
    }
}