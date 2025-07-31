using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("SendSMSEmailOnVideoCall")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SendVideoCallLink([FromBody] SendVideoCallLinkDto dto)
        {
            var result = RateLimit(dto, "send_video_call_link");
            return result ?? await RunService(201, dto, _processAccountsService.SendVideoCallLink);
        }
    }
}