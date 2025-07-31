using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class PublicController : FlexControllerBridge<PublicController>
    {
        [HttpPost]
        [AuthorizeAPI]
        [Route("mvp/import/accounts/{tenantId}")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ImportAccounts(string tenantId, [FromBody] ImportAccountsDto dto)
        {
            dto.CustomId = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            return await RunService(201, dto, _processPublicService.ImportAccounts);
        }
    }
}