using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/citiesbystate")]
        [ProducesResponseType(typeof(IEnumerable<GetCitiesByStateIdDto>), 200)]
        public async Task<IActionResult> GetCitiesByStateId(string stateId)
        {
            GetCitiesByStateIdParams parameters = new GetCitiesByStateIdParams();
            parameters.stateId = stateId;
            return await RunQueryListServiceAsync<GetCitiesByStateIdParams, GetCitiesByStateIdDto>(
                        parameters, _processGeographyService.GetCitiesByStateId);
        }
    }
}