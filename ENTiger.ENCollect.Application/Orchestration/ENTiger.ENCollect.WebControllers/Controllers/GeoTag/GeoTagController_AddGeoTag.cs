using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GeoTagController : FlexControllerBridge<GeoTagController>
    {
        [HttpPost]
        [Route("add/geoTagDetails")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddGeoTag([FromBody] AddGeoTagDto dto)
        {
            var result = RateLimit(dto, "add_geotag");
            return result ?? await RunService(201, dto, _processGeoTagService.AddGeoTag);
        }
    }
}