using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("search/AreaPinCodeMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchAreaPinCodesDto>), 200)]
        public async Task<IActionResult> SearchAreaPinCodes(string search)
        {
            SearchAreaPinCodesParams parameters = new SearchAreaPinCodesParams() { query = search };
            return await RunQueryListServiceAsync<SearchAreaPinCodesParams, SearchAreaPinCodesDto>(parameters, _processGeographyService.SearchAreaPinCodes);
        }
    }
}