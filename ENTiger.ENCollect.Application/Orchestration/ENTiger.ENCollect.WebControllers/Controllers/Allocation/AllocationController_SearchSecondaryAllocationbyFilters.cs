using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("allocation/secondary/agent/search")]
        [ProducesResponseType(typeof(SearchSecondaryAllocationbyFiltersDto), 200)]
        public async Task<IActionResult> SearchSecondaryAllocationbyFilters([FromBody] SearchSecondaryAllocationbyFiltersParams parameters)
        {
            return await RunQuerySingleServiceAsync<SearchSecondaryAllocationbyFiltersParams, SearchSecondaryAllocationbyFiltersDto>(parameters, _processAllocationService.SearchSecondaryAllocationbyFilters);
        }
    }
}