using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/statesbyregion")]
        [ProducesResponseType(typeof(IEnumerable<GetStatesByRegionIdDto>), 200)]
        public async Task<IActionResult> GetStatesByRegionId(string regionId)
        {
            GetStatesByRegionIdParams parameters = new GetStatesByRegionIdParams();
            parameters.regionId = regionId;
            return await RunQueryListServiceAsync<GetStatesByRegionIdParams, GetStatesByRegionIdDto>(
                        parameters, _processGeographyService.GetStatesByRegionId);
        }
    }
}