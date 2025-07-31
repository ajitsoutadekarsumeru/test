using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [Authorize]
        [ServiceFilter(typeof(CheckUserSessionAccess))]
        [HttpGet()]
        [Route("mvp/Search/Agent/byname/{name}")]
        [ProducesResponseType(typeof(IEnumerable<GetUsersByNameDto>), 200)]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            GetUsersByNameParams parameters = new GetUsersByNameParams() { Name = name };
            return await RunQueryListServiceAsync<GetUsersByNameParams, GetUsersByNameDto>(parameters, _processApplicationUsersService.GetUsersByName);
        }
    }
}