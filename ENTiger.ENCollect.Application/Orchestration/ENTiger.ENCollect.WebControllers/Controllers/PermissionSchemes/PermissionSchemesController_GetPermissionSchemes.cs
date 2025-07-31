using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class PermissionSchemesController : FlexControllerBridge<PermissionSchemesController>
    {
        [HttpGet()]
        [Route("permission/scheme/list")]
        [Authorize(Policy = "CanViewPermissionSchemesPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetPermissionSchemesDto>), 200)]
        public async Task<IActionResult> GetPermissionSchemes([FromQuery]GetPermissionSchemesParams parameters)
        {
            return await RunQueryListServiceAsync<GetPermissionSchemesParams, GetPermissionSchemesDto>(
                        parameters, _processPermissionSchemesService.GetPermissionSchemes);
        }
    }
}
