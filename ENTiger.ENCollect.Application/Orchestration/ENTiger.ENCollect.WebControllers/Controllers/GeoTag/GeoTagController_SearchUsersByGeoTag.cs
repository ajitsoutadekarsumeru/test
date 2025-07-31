using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GeoTagController : FlexControllerBridge<GeoTagController>
    {
        [HttpPost]
        [Route("geosearchusercurrentlocation")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SearchUsersByGeoTag([FromBody] SearchUsersByGeoTagDto dto)
        {
            return await RunService(201, dto, _processGeoTagService.SearchUsersByGeoTag);
        }
    }
}