using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("segment/view")]
        [ProducesResponseType(typeof(GetSegmentByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSegmentById(string Id)
        {
            GetSegmentByIdParams parameters = new GetSegmentByIdParams();
            parameters.Id = Id;

            return await RunQuerySingleServiceAsync<GetSegmentByIdParams, GetSegmentByIdDto>(
                        parameters, _processSegmentationService.GetSegmentById);
        }
    }
}