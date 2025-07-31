using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("account/customer/response/consent")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> CustomerConsentResponse([FromBody]CustomerConsentResponseDto dto)
        {
            return await RunService(200, dto, _processAccountsService.CustomerConsentResponseAsync);
        }
    }
}
