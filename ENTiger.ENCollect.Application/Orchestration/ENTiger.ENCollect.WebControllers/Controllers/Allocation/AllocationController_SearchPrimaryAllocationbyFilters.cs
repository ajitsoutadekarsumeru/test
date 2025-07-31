using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("allocation/primary/agencyfilter")]
        [Authorize(Policy = "CanUpdateSecondaryAllocationByFilterPolicy")]
        [ProducesResponseType(typeof(SearchPrimaryAllocationbyFiltersDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPrimaryAllocationbyFilters([FromBody] SearchPrimaryAllocationbyFiltersParams parameters)
        {
            return await RunQuerySingleServiceAsync<SearchPrimaryAllocationbyFiltersParams, SearchPrimaryAllocationbyFiltersDto>(parameters, _processAllocationService.SearchPrimaryAllocationbyFilters);
        }
    }
}