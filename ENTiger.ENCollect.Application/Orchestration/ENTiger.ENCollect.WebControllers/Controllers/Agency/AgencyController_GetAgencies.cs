using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("Agency")]
        [ProducesResponseType(typeof(IEnumerable<GetAgenciesDto>), 200)]
        public async Task<IActionResult> GetAgencies()
        {
            GetAgenciesParams parameters = new GetAgenciesParams();
            return await RunQueryListServiceAsync<GetAgenciesParams, GetAgenciesDto>(parameters, _processAgencyService.GetAgencies);
        }
    }
}