using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("agencytypes")]
        [ProducesResponseType(typeof(IEnumerable<GetAgencyTypesDto>), 200)]
        public async Task<IActionResult> GetAgencyTypes([FromQuery] GetAgencyTypesParams parameters)
        {
            return await RunQueryListServiceAsync<GetAgencyTypesParams, GetAgencyTypesDto>(
                        parameters, _processAgencyService.GetAgencyTypes);
        }
    }
}