using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpGet()]
        [Route("staff/collection/{id}")]
        [Authorize(Policy = "CanViewStaffPolicy")]
        [ProducesResponseType(typeof(CompanyUserDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompanyUserDetails(string id)
        {
            CompanyUserDetailsParams parameters = new CompanyUserDetailsParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<CompanyUserDetailsParams, CompanyUserDetailsDto>(
                        parameters, _processCompanyUsersService.CompanyUserDetails);
        }
    }
}