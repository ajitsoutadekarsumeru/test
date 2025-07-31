using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/cities")]
        [ProducesResponseType(typeof(IEnumerable<GetCitieDto>), 200)]
        public async Task<IActionResult> GetCities([FromQuery] GetCitiesParam parameters)
        {
            return await RunQueryListServiceAsync<GetCitiesParam, GetCitieDto>(
                        parameters, _processGeographyService.GetCities);
        }
    }
}