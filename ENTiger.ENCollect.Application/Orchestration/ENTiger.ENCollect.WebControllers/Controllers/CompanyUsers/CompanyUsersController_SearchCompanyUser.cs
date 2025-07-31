using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPost()]
        [Route("staff/collection/search")]
        [Authorize(Policy = "CanSearchStaffPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchCompanyUserDto>), 200)]
        public async Task<IActionResult> SearchCompanyUser([FromBody] SearchCompanyUserParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchCompanyUserParams, SearchCompanyUserDto>(parameters, _processCompanyUsersService.SearchCompanyUser);
        }
    }
}