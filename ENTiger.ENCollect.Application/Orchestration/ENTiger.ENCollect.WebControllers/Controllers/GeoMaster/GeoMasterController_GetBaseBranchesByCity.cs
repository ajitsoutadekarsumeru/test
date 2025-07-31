using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpPost]
        [Route("master/citybasedbranch")]
        [ProducesResponseType(typeof(IEnumerable<GetBaseBranchesByCityDto>), 200)]
        public async Task<IActionResult> GetBaseBranchesByCity([FromBody] GetBaseBranchesByCityParams parameters)
        {
            return await RunQueryListServiceAsync<GetBaseBranchesByCityParams, GetBaseBranchesByCityDto>(parameters, _processGeoMasterService.GetBaseBranchesByCity);
        }
    }
}