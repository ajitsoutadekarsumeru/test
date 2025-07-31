using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    public partial class PermissionSchemesController : FlexControllerBridge<PermissionSchemesController>
    {
        [HttpPost()]
        [Route("permission/scheme/designations")]
        [Authorize(Policy = "CanViewPermissionSchemeDesignationsPolicy")]
        [ProducesResponseType(typeof(GetDesignationDetailsBySchemeIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDesignationDetailsBySchemeId([FromBody] GetDesignationDetailsBySchemeIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetDesignationDetailsBySchemeIdParams, GetDesignationDetailsBySchemeIdDto>(
                        parameters, _processPermissionSchemesService.GetDesignationDetailsBySchemeId);
        }
    }
}
