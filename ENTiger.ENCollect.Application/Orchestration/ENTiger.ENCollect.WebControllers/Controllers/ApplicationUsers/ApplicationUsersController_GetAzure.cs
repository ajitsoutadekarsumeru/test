using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpGet]
        [Route("Account/get/azure")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetAzure()
        {
            GetAzureDto dto = new GetAzureDto();
            return await RunService(200, dto, _processApplicationUsersService.GetAzure);
        }
    }
}