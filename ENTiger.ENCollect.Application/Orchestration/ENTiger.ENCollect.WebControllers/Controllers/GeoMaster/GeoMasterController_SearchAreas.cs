using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpPost()]
        [Route("Master/Area")]
        [ProducesResponseType(typeof(IEnumerable<SearchAreasDto>), 200)]
        public async Task<IActionResult> SearchAreas([FromBody] SearchAreasParams parameters)
        {
            return await RunQueryListServiceAsync<SearchAreasParams, SearchAreasDto>(parameters, _processGeoMasterService.SearchAreas);
        }
    }
}