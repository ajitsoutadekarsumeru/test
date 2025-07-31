using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpGet()]
        [Route("search/DesignationMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchDesignationDto>), 200)]
        public async Task<IActionResult> SearchDesignation(string search)
        {
            SearchDesignationParams parameters = new SearchDesignationParams();
            parameters.search = search;
            return await RunQueryListServiceAsync<SearchDesignationParams, SearchDesignationDto>(
                        parameters, _processDesignationsService.SearchDesignation);
        }
    }
}