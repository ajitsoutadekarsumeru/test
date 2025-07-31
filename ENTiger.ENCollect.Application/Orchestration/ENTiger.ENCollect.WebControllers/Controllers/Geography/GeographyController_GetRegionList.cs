using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/regions")]
        [ProducesResponseType(typeof(IEnumerable<GetRegionListDto>), 200)]
        public async Task<IActionResult> GetRegionList([FromBody] GetRegionListParams parameters)
        {
            return await RunQueryListServiceAsync<GetRegionListParams, GetRegionListDto>(
                        parameters, _processGeographyService.GetRegionList);
        }
    }
}