using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("get/segment/advancefields")]
        [ProducesResponseType(typeof(IEnumerable<GetSegmentAdvanceFieldsDto>), 200)]
        public async Task<IActionResult> GetSegmentAdvanceFields([FromQuery] GetSegmentAdvanceFieldsParams parameters)
        {
            return await RunQueryListServiceAsync<GetSegmentAdvanceFieldsParams, GetSegmentAdvanceFieldsDto>(
                        parameters, _processSegmentationService.GetSegmentAdvanceFields);
        }
    }
}