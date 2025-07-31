using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpGet()]
        [Route("companyuser/department/list/{IsFrontEndStaff}")]
        [ProducesResponseType(typeof(CompanyUserDepartmentsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompanyUserDepartments([FromQuery] CompanyUserDepartmentsParams parameters, bool? IsFrontEndStaff)
        {
            parameters.IsFrontEndStaff = IsFrontEndStaff;
            return await RunQueryListServiceAsync<CompanyUserDepartmentsParams, CompanyUserDepartmentsDto>(parameters, _processCompanyUsersService.CompanyUserDepartments);
        }
    }
}