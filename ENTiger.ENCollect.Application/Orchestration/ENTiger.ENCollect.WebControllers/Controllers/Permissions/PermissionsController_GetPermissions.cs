using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PermissionsModule
{
    public partial class PermissionsController : FlexControllerBridge<PermissionsController>
    {
        [HttpGet()]
        [Route("permission/list")]
        [Authorize(Policy = "CanViewPermissionsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetPermissionsDto>), 200)]
        public async Task<IActionResult> GetPermissions([FromQuery]GetPermissionsParams parameters)
        {
            return await RunQueryListServiceAsync<GetPermissionsParams, GetPermissionsDto>(
                        parameters, _processPermissionsService.GetPermissions);
        }
    }
}
