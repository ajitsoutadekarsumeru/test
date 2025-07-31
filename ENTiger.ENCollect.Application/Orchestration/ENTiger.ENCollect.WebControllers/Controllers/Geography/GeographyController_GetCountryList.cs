using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/countries")]
        [ProducesResponseType(typeof(IEnumerable<GetCountryListDto>), 200)]
        public async Task<IActionResult> GetCountryList([FromQuery] GetCountryListParams parameters)
        {
            return await RunQueryListServiceAsync<GetCountryListParams, GetCountryListDto>(
                        parameters, _processGeographyService.GetCountryList);
        }
    }
}