using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{

    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [Authorize]
        [ServiceFilter(typeof(CheckUserSessionAccess))]
        [HttpPost()]
        [Route("um/users/batch")]
        [ProducesResponseType(typeof(IEnumerable<GetUsersByIdsDto>), 200)]
        public async Task<IActionResult> GetUsersByIds([FromBody]GetUsersByIdsParams parameters)
        {
            return await RunQueryListServiceAsync<GetUsersByIdsParams, GetUsersByIdsDto>(parameters, _processApplicationUsersService.GetUsersByIds);
        }
    }    
}
