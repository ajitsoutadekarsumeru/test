using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeoMasterModule
{
    public partial class GeoMasterController : FlexControllerBridge<GeoMasterController>
    {
        [HttpPost()]
        [Route("Master/BaseBranch")]
        [ProducesResponseType(typeof(IEnumerable<SearchBaseBranchesDto>), 200)]
        public async Task<IActionResult> SearchBaseBranches([FromBody] SearchBaseBranchesParams parameters)
        {
            return await RunQueryListServiceAsync<SearchBaseBranchesParams, SearchBaseBranchesDto>(parameters, _processGeoMasterService.SearchBaseBranches);
        }
    }
}