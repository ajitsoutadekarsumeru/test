using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPut]
        [Route("staff/collection/edit")]
        [Authorize(Policy = "CanUpdateStaffPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateCompanyUser([FromBody] UpdateCompanyUserDto dto)
        {
            var result = RateLimit(dto, "update_staff");
            return result ?? await RunService(200, dto, _processCompanyUsersService.UpdateCompanyUser);
        }
    }
}