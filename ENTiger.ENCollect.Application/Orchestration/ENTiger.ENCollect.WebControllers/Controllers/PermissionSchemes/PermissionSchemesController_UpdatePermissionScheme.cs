using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class PermissionSchemesController : FlexControllerBridge<PermissionSchemesController>
    {
        [HttpPut]
        [Route("permission/scheme/update")]
        [Authorize(Policy = "CanUpdatePermissionSchemePolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdatePermissionScheme([FromBody]UpdatePermissionSchemeDto dto)
        {
            return await RunService(200, dto, _processPermissionSchemesService.UpdatePermissionScheme);
        }
    }
}
