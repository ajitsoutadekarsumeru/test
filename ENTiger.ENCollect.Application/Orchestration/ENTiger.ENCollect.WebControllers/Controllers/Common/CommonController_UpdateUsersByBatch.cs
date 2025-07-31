using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/users/update/batch")]
        [Authorize(Policy = "CanUploadBulkEnableDisableUserPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> UpdateUsersByBatch([FromBody] UpdateUsersByBatchDto dto)
        {
            var result = RateLimit(dto, "upload_users_update");
            return result ?? await RunService(201, dto, _processCommonService.UpdateUsersByBatch);
        }
    }
}