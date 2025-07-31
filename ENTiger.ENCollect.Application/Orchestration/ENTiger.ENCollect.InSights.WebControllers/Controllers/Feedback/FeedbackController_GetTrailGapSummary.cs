using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpGet()]
        [Route("trailgap/summary")]
        [ProducesResponseType(typeof(IEnumerable<GetTrailGapSummaryDto>), 200)]
        public async Task<IActionResult> GetTrailGapSummary([FromQuery]GetTrailGapSummaryParams parameters)
        {
            return await RunQueryListServiceAsync<GetTrailGapSummaryParams, GetTrailGapSummaryDto>(
                        parameters, _processFeedbackService.GetTrailGapSummary);
        }
    }
}
