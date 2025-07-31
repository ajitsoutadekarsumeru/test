using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/Login")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, [FromHeader(Name = "g-recaptcha-token")] string? token)
        {
            dto.Token = token;
            if (_googleSettings.Captcha.Enabled && string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("Error", "Token is required");
                return BadRequest(ModelState);
            }
            return await RunService(200, dto, _processApplicationUsersService.Login);
        }
    }
}