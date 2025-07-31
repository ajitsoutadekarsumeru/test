using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPut]
        [Route("companyusers/enable")]
        [Authorize(Policy = "CanEnableStaffPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> EnableCompanyUsers([FromBody]EnableCompanyUsersDto dto)
        {
            return await RunService(200, dto, _processCompanyUsersService.EnableCompanyUsers);
        }
    }
}
