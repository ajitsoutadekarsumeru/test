using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpPost()]
        [Route("Master/State")]
        [ProducesResponseType(typeof(IEnumerable<SearchStatesDto>), 200)]
        public async Task<IActionResult> SearchStates([FromBody] SearchStatesParams parameters)
        {
            return await RunQueryListServiceAsync<SearchStatesParams, SearchStatesDto>(parameters, _processGeoMasterService.SearchStates);
        }
    }
}