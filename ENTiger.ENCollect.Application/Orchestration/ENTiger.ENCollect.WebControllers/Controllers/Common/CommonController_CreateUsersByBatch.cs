using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/users/create/batch")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> CreateUsersByBatch([FromBody] CreateUsersByBatchDto dto)
        {
            var result = RateLimit(dto, "upload_users_create");
            return result ?? await RunService(201, dto, _processCommonService.CreateUsersByBatch);
        }
    }
}