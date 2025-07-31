using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpGet()]
        [Route("Master/All/City")]
        [ProducesResponseType(typeof(IEnumerable<GetCitiesDto>), 200)]
        public async Task<IActionResult> GetCities([FromBody] GetCitiesParams parameters)
        {
            return await RunQueryListServiceAsync<GetCitiesParams, GetCitiesDto>(parameters, _processGeoMasterService.GetCities);
        }
    }
}