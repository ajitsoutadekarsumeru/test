using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpGet()]
        [Route("account/search/role/scope")]
        [ProducesResponseType(typeof(IEnumerable<GetRoleBasedSearchConfigDto>), 200)]
        public async Task<IActionResult> GetRoleBasedSearchConfig([FromQuery]GetRoleBasedSearchConfigParams parameters)
        {
            return await RunQueryListServiceAsync<GetRoleBasedSearchConfigParams, GetRoleBasedSearchConfigDto>(
                        parameters, _processAccountsService.GetRoleBasedSearchConfig);
        }
    }
}
