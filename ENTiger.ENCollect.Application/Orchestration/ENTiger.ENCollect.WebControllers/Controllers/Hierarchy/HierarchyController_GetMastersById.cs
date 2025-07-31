using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.HierarchyModule
{
    public partial class HierarchyController : FlexControllerBridge<HierarchyController>
    {
        [HttpPost]
        [Route("hierarchy/product/masters")]
        [ProducesResponseType(typeof(IEnumerable<GetMastersByIdDto>), 200)]
        public async Task<IActionResult> GetProductMastersByLevelId([FromBody] GetMastersByIdParams parameters)
        {
            return await RunQueryListServiceAsync<GetMastersByIdParams, GetMastersByIdDto>(
                        parameters, _processHierarchyService.GetMastersById);
        }

        [HttpPost]
        [Route("hierarchy/geo/masters")]
        [ProducesResponseType(typeof(IEnumerable<GetMastersByIdDto>), 200)]
        public async Task<IActionResult> GetGeoMastersByLevelId([FromBody] GetMastersByIdParams parameters)
        {
            return await RunQueryListServiceAsync<GetMastersByIdParams, GetMastersByIdDto>(
                        parameters, _processHierarchyService.GetMastersById);
        }
    }
}
