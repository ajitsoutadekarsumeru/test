using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPost]
        [Route("search/agencies")]
        [Authorize(Policy = "CanSearchAgencyPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchAgenciesDto>), 200)]
        public async Task<IActionResult> SearchAgencies([FromBody] SearchAgenciesParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchAgenciesParams, SearchAgenciesDto>(parameters, _processAgencyService.SearchAgencies);
        }
    }
}