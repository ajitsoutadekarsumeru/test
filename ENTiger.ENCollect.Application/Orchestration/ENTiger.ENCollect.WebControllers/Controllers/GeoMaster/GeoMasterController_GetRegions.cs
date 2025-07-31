using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpGet()]
        [Route("Master/All/Region")]
        [ProducesResponseType(typeof(IEnumerable<GetRegionsDto>), 200)]
        public async Task<IActionResult> GetRegions()
        {
            GetRegionsParams parameters = new GetRegionsParams();
            return await RunQueryListServiceAsync<GetRegionsParams, GetRegionsDto>(parameters, _processGeoMasterService.GetRegions);
        }
    }
}