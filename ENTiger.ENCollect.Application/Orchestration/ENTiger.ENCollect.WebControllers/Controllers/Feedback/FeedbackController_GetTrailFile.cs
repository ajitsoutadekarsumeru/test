using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost]
        [Route("bulktrail/getfile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetTrailFile([FromBody]GetTrailFileDto dto)
        {
            var result = await RunService(200, dto, _processFeedbackService.GetTrailFile);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName, (result as ObjectResult).Value.ToString());
            }
            return result;
        }
    }
}
