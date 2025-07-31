using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpPut]
        [Route("assignscheme")]
        [Authorize(Policy = "CanAssignPermissionSchemePolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> AssignScheme([FromBody]AssignSchemeDto dto)
        {
            return await RunService(200, dto, _processDesignationsService.AssignScheme);
        }
    }
}
