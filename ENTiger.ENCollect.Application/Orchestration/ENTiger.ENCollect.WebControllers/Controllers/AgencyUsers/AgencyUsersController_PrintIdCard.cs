using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpGet]
        [Route("printidcard/{id}")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> PrintIdCard(string Id)
        {
            PrintIdCardDto dto = new PrintIdCardDto() { Id = Id };
            return await RunService(200, dto, _processAgencyUsersService.PrintIdCard);
        }
    }
}