using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class PermissionSchemesController : FlexControllerBridge<PermissionSchemesController>
    {
        [HttpPost]
        [Route("permission/scheme/create")]
        [Authorize(Policy = "CanCreatePermissionSchemePolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> CreatePermissionScheme([FromBody]CreatePermissionSchemeDto dto)
        {
            return await RunService(201, dto, _processPermissionSchemesService.CreatePermissionScheme);
        }
    }
}
