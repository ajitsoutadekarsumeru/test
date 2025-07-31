using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/users/update/batch/status")]
        [Authorize(Policy = "CanSearchBulkEnableDisableUserStatusPolicy")]
        [ProducesResponseType(typeof(IEnumerable<SearchUpdateUsersByBatchStatusDto>), 200)]
        public async Task<IActionResult> SearchUpdateUsersByBatchStatus([FromBody] SearchUpdateUsersByBatchStatusParams parameters)
        {
            return await RunQueryListServiceAsync<SearchUpdateUsersByBatchStatusParams, SearchUpdateUsersByBatchStatusDto>(
                        parameters, _processCommonService.SearchUpdateUsersByBatchStatus);
        }
    }
}