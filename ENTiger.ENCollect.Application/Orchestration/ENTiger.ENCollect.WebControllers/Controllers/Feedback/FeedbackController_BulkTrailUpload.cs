using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost]
        [Route("feedback/BulkTrailUpload")]
        [Authorize(Policy = "CanUploadBulkTrailPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> BulkTrailUpload([FromBody] BulkTrailUploadDto dto)
        {
            dto.Customid = DateTime.Now.ToString("MMddyyyyhhmmssfff");
            var result = RateLimit(dto, "upload_bulktrail");
            return result ?? await RunService(201, dto, _processFeedbackService.BulkTrailUpload);
        }
    }
}