using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpGet()]
        [Route("Master/All/Area")]
        [ProducesResponseType(typeof(IEnumerable<GetAreasDto>), 200)]
        public async Task<IActionResult> GetAreas()
        {
            GetAreasParams parameters = new GetAreasParams();
            return await RunQueryListServiceAsync<GetAreasParams, GetAreasDto>(parameters, _processGeoMasterService.GetAreas);
        }
    }
}