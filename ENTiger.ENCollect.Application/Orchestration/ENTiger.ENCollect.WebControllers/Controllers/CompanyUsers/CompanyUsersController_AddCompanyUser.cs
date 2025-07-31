using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPost]
        [Route("staff/collection/create")]
        [Authorize(Policy = "CanCreateStaffPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddCompanyUser([FromBody] AddCompanyUserDto dto)
        {
            var result = RateLimit(dto, "create_staff");
            return result ?? await RunService(201, dto, _processCompanyUsersService.AddCompanyUser);
        }
    }
}