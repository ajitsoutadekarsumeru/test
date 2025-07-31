using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionsModule
{

    public partial class PermissionsController : FlexControllerBridge<PermissionsController>
    {
        [HttpPost()]
        [Route("permission/search")]
        [Authorize(Policy = "CanSearchPermissionsPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchPermissionsDto>), 200)]
        public async Task<IActionResult> SearchPermissions([FromBody]SearchPermissionsParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchPermissionsParams, SearchPermissionsDto>(parameters, _processPermissionsService.SearchPermissions);
        }

    }

    
}
