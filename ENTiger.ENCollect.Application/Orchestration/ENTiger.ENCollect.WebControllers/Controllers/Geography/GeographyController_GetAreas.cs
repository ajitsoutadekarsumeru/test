using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/areas")]
        [ProducesResponseType(typeof(IEnumerable<GetAreaDto>), 200)]
        public async Task<IActionResult> GetAreas([FromQuery] GetAreasParams parameters)
        {
            return await RunQueryListServiceAsync<GetAreasParams, GetAreaDto>(parameters, _processGeographyService.GetAreas);
        }
    }
}