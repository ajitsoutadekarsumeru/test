using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GeoTagController : FlexControllerBridge<GeoTagController>
    {
        [HttpPost]
        [Route("add/LoginFirstgeoTagDetails")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddLoginGeoTag([FromBody] AddLoginGeoTagDto dto)
        {
            var result = RateLimit(dto, "add_firstlogin_geotag");
            return result ?? await RunService(201, dto, _processGeoTagService.AddLoginGeoTag);
        }
    }
}