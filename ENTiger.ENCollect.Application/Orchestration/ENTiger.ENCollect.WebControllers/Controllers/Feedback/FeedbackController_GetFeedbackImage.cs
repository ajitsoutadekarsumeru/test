using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpGet]
        [Route("account/feedback/getimage")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetFeedbackImage(string fileName)
        {
            GetFeedbackImageDto dto = new GetFeedbackImageDto() { FileName = fileName };
            var result = await RunService(200, dto, _processFeedbackService.GetFeedbackImage);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
            }
            return result;
        }
    }
}