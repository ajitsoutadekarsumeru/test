using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpGet()]
        [Route("Master/Country")]
        [ProducesResponseType(typeof(IEnumerable<GetCountriesDto>), 200)]
        public async Task<IActionResult> GetCountries()
        {
            GetCountriesParams parameters = new GetCountriesParams();
            return await RunQueryListServiceAsync<GetCountriesParams, GetCountriesDto>(parameters, _processGeoMasterService.GetCountries);
        }
    }
}