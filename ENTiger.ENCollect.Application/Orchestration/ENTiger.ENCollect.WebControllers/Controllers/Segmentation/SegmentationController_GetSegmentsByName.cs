using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("segment/byname")]
        [ProducesResponseType(typeof(IEnumerable<GetSegmentsByNameDto>), 200)]
        public async Task<IActionResult> GetSegmentsByName([FromQuery] string name)
        {
            GetSegmentsByNameParams parameters = new GetSegmentsByNameParams();
            parameters.name = name;

            return await RunQueryListServiceAsync<GetSegmentsByNameParams, GetSegmentsByNameDto>(
                        parameters, _processSegmentationService.GetSegmentsByName);
        }
    }
}