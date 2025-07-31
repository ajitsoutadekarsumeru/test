using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("agencies/byname/{name}")]
        [ProducesResponseType(typeof(IEnumerable<GetAgenciesByNameDto>), 200)]
        public async Task<IActionResult> GetAgenciesByName(string name)
        {
            GetAgenciesByNameParams parameters = new GetAgenciesByNameParams();
            parameters.Name = name;
            return await RunQueryListServiceAsync<GetAgenciesByNameParams, GetAgenciesByNameDto>(
                        parameters, _processAgencyService.GetAgenciesByName);
        }
    }
}