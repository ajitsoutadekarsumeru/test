using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpGet]
        [Route("account/GetKey")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetKey()
        {
            GetKeyDto dto = new GetKeyDto();
            return await RunService(200, dto, _processApplicationUsersService.GetKey);
        }
    }
}