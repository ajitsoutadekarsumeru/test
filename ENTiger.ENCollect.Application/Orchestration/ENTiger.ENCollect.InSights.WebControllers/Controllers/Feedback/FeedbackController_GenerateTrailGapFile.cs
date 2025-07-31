using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.FeedbackModule
{

    public partial class FeedbackController : FlexControllerBridge<FeedbackController>
    {
        [HttpPost()]
        [Route("trailgap/file/generate")]
        [ProducesResponseType(typeof(GenerateTrailGapFileDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateTrailGapFile([FromBody]GenerateTrailGapFileParams parameters)
        {
            return RunQuerySingleService<GenerateTrailGapFileParams, GenerateTrailGapFileDto>(
                        parameters, _processFeedbackService.GenerateTrailGapFile);
        }
    }
}
