using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GeoTagController : FlexControllerBridge<GeoTagController>
    {
        [HttpPost()]
        [Route("get/geoTagDetails")]
        [ProducesResponseType(typeof(IEnumerable<GetGeoTagDetailsDto>), 200)]
        public async Task<IActionResult> GetGeoTagDetails([FromBody] GetGeoTagDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetGeoTagDetailsParams, GetGeoTagDetailsDto>(
                        parameters, _processGeoTagService.GetGeoTagDetails);
        }
    }
}