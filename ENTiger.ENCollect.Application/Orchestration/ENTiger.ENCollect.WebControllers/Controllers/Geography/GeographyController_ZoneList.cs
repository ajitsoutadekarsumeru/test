using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/zones")]
        [ProducesResponseType(typeof(IEnumerable<ZoneListDto>), 200)]
        public async Task<IActionResult> ZoneList([FromQuery] ZoneListParams parameters)
        {
            return await RunQueryListServiceAsync<ZoneListParams, ZoneListDto>(
                        parameters, _processGeographyService.ZoneList);
        }
    }
}