using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpGet()]
        [Route("Master/All/State")]
        [ProducesResponseType(typeof(IEnumerable<GetStatesDto>), 200)]
        public async Task<IActionResult> GetStates()
        {
            GetStatesParams parameters = new GetStatesParams();
            return await RunQueryListServiceAsync<GetStatesParams, GetStatesDto>(parameters, _processGeoMasterService.GetStates);
        }
    }
}