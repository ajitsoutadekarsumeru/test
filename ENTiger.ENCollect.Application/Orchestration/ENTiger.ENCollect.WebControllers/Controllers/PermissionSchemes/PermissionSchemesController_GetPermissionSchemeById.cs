using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PermissionSchemesModule
{

    public partial class PermissionSchemesController : FlexControllerBridge<PermissionSchemesController>
    {
        [HttpPost()]
        [Route("permission/scheme")]
        [Authorize(Policy = "CanViewPermissionSchemePolicy")]
        [ProducesResponseType(typeof(GetPermissionSchemeByIdDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPermissionSchemeById([FromBody]GetPermissionSchemeByIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetPermissionSchemeByIdParams, GetPermissionSchemeByIdDto>(
                        parameters, _processPermissionSchemesService.GetPermissionSchemeById);
        }
    }
}
