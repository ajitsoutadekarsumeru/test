using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("TCAgency")]
        [ProducesResponseType(typeof(IEnumerable<GetTCAgenciesDto>), 200)]
        public async Task<IActionResult> GetTCAgencies()
        {
            GetTCAgenciesParams parameters = new GetTCAgenciesParams();
            return await RunQueryListServiceAsync<GetTCAgenciesParams, GetTCAgenciesDto>(parameters, _processAgencyService.GetTCAgencies);
        }
    }
}