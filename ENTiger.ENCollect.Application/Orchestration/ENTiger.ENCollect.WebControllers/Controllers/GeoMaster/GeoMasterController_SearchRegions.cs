using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpPost()]
        [Route("Master/Region")]
        [ProducesResponseType(typeof(IEnumerable<SearchRegionsDto>), 200)]
        public async Task<IActionResult> SearchRegions([FromBody] SearchRegionsParams parameters)
        {
            return await RunQueryListServiceAsync<SearchRegionsParams, SearchRegionsDto>(parameters, _processGeoMasterService.SearchRegions);
        }
    }
}