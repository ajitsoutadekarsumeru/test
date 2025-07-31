using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("getagencysbyagencytype")]
        [ProducesResponseType(typeof(IEnumerable<GetAgenciesByTypeIdDto>), 200)]
        public async Task<IActionResult> GetAgenciesByTypeId([FromQuery] GetAgenciesByTypeIdParams parameters)
        {
            return await RunQueryListServiceAsync<GetAgenciesByTypeIdParams, GetAgenciesByTypeIdDto>(
                        parameters, _processAgencyService.GetAgenciesByTypeId);
        }
    }
}