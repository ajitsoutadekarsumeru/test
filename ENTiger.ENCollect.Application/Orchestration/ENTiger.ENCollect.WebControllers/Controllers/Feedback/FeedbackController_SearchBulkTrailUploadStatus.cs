using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost]
        [Route("feedback/BulkTrailStatus")]
        [Authorize(Policy = "CanSearchBulkTrailStatusPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchBulkTrailUploadStatusDto>), 200)]
        public async Task<IActionResult> SearchBulkTrailUploadStatus([FromBody] SearchBulkTrailUploadStatusParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchBulkTrailUploadStatusParams, SearchBulkTrailUploadStatusDto>(parameters, _processFeedbackService.SearchBulkTrailUploadStatus);
        }
    }
}