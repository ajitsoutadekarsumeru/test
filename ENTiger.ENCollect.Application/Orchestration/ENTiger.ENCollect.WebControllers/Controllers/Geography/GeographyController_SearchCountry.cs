using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("search/CountryMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchCountryDto>), 200)]
        public async Task<IActionResult> SearchCountry(string search)
        {
            SearchCountryParams parameters = new SearchCountryParams();
            parameters.search = search;
            return await RunQueryListServiceAsync<SearchCountryParams, SearchCountryDto>(
                        parameters, _processGeographyService.SearchCountry);
        }
    }
}