using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPut]
        [Route("staff/collection/approve")]
        [Authorize(Policy = "CanApproveStaffPolicy")]        
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ApproveCompanyUser([FromBody] ApproveCompanyUserDto dto)
        {
            var result = RateLimit(dto, "approve_staff");
            return result ?? await RunService(200, dto, _processCompanyUsersService.ApproveCompanyUser);
        }
    }
}