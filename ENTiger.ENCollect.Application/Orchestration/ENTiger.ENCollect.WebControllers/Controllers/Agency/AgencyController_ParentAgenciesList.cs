using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("parent/agency")]
        [ProducesResponseType(typeof(IEnumerable<ParentAgenciesListDto>), 200)]
        public async Task<IActionResult> ParentAgenciesList([FromQuery] ParentAgenciesListParams parameters)
        {
            return await RunQueryListServiceAsync<ParentAgenciesListParams, ParentAgenciesListDto>(
                        parameters, _processAgencyService.ParentAgenciesList);
        }
    }
}