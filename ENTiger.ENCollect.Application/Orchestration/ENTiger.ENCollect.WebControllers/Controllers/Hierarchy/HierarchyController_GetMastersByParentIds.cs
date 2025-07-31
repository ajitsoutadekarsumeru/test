using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.HierarchyModule
{
    public partial class HierarchyController : FlexControllerBridge<HierarchyController>
    {
        [HttpPost]
        [Route("hierarchy/product/masters/children")]
        [ProducesResponseType(typeof(IEnumerable<GetMastersByParentIdsDto>), 200)]
        public async Task<IActionResult> GetProductMastersByParentIds([FromBody] GetMastersByParentIdsParams parameters)
        {
            return await RunQueryListServiceAsync<GetMastersByParentIdsParams, GetMastersByParentIdsDto>(
                        parameters, _processHierarchyService.GetMastersByParentIds);
        }

        [HttpPost]
        [Route("hierarchy/geo/masters/children")]
        [ProducesResponseType(typeof(IEnumerable<GetMastersByParentIdsDto>), 200)]
        public async Task<IActionResult> GetGeoMastersByParentIds([FromBody] GetMastersByParentIdsParams parameters)
        {
            return await RunQueryListServiceAsync<GetMastersByParentIdsParams, GetMastersByParentIdsDto>(
                        parameters, _processHierarchyService.GetMastersByParentIds);
        }
    }
}
