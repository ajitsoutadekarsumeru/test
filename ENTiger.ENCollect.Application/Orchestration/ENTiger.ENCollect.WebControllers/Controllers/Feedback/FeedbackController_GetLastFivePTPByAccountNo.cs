using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost()]
        [Route("account/feedback/ptp/LastFive")]
        [Authorize(Policy = "CanViewLastFivePTPsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetLastFivePTPByAccountNoDto>), 200)]
        public async Task<IActionResult> GetLastFivePTPByAccountNo([FromBody] GetLastFivePTPByAccountNoParams parameters)
        {
            return await RunQueryListServiceAsync<GetLastFivePTPByAccountNoParams, GetLastFivePTPByAccountNoDto>(
                        parameters, _processFeedbackService.GetLastFivePTPByAccountNo);
        }
    }
}