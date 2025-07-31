using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPut]
        [Route("staff/collection/deactivate")]
        [Authorize(Policy = "CanDisableStaffPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeactivateCompanyUser([FromBody] DeactivateCompanyUserDto dto)
        {
            var result = RateLimit(dto, "deactivate_staff");
            return result ?? await RunService(200, dto, _processCompanyUsersService.DeactivateCompanyUser);
        }
    }
}