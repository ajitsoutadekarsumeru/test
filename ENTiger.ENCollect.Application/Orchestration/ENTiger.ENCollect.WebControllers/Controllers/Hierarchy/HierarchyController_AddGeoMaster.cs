using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.HierarchyModule
{
    public partial class HierarchyController : FlexControllerBridge<HierarchyController>
    {
        [HttpPost]
        [Route("hierarchy/geo/master/add")]
        [Authorize(Policy = "CanCreateGeoMasterPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddGeoMaster([FromBody]AddGeoMasterDto dto)
        {
            var result = RateLimit(dto, "add_geo_master");
            return result ?? await RunService(201, dto, _processHierarchyService.AddGeoMaster);
        }
    }
}
