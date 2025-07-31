using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost()]
        [Route("account/feedback/last/list")]
        [Authorize(Policy = "CanViewFeedbacksPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<GetFeedbacksByAccountNoDto>), 200)]
        public async Task<IActionResult> GetFeedbacksByAccountNo([FromBody] GetFeedbacksByAccountNoParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetFeedbacksByAccountNoParams, GetFeedbacksByAccountNoDto>(parameters, _processFeedbackService.GetFeedbacksByAccountNo);
        }
    }
}