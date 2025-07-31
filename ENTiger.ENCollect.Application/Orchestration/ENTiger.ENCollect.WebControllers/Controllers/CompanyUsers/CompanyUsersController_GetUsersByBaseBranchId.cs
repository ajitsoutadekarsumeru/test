using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class CompanyUsersController : FlexControllerBridge<CompanyUsersController>
    {
        [HttpPost()]
        [Route("get/collectionstaffbybasebranch")]
        [ProducesResponseType(typeof(IEnumerable<GetUsersByBaseBranchIdDto>), 200)]
        public async Task<IActionResult> GetUsersByBaseBranchId([FromBody] GetUsersByBaseBranchIdParams parameters)
        {
            return await RunQueryListServiceAsync<GetUsersByBaseBranchIdParams, GetUsersByBaseBranchIdDto>(
                        parameters, _processCompanyUsersService.GetUsersByBaseBranchId);
        }
    }
}