using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPut]
        [Route("staff/collection/reject")]
        [Authorize(Policy = "CanRejectStaffPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> RejectCompanyUser([FromBody] RejectCompanyUserDto dto)
        {
            var result = RateLimit(dto, "reject_staff");
            return result ?? await RunService(200, dto, _processCompanyUsersService.RejectCompanyUser);
        }
    }
}