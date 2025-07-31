using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("search/StateMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchStateDto>), 200)]
        public async Task<IActionResult> SearchState(string search)
        {
            SearchStateParams parameters = new SearchStateParams();
            parameters.search = search;

            return await RunQueryListServiceAsync<SearchStateParams, SearchStateDto>(
                        parameters, _processGeographyService.SearchState);
        }
    }
}