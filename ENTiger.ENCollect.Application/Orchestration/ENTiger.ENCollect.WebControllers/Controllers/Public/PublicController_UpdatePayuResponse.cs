using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class PublicController : FlexControllerBridge<PublicController>
    {
        [HttpPost]
        [Route("mvp/pg/payu/response/{tenantId}")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdatePayuResponse(string? tenantId, [FromForm] UpdatePayuResponseDto dto)
        {
            // Add the tenantId to the request headers
            if (!string.IsNullOrEmpty(tenantId))
            {
                // Add the tenantId to the current request's headers
                HttpContext.Request.Headers["TenantId"] = tenantId;
            }
            else
            {
                return BadRequest("TenantId is required in the URL.");
            }

            // Continue with processing the request
            return await RunService(200, dto, _processPublicService.UpdatePayuResponse);
        }
    }
}