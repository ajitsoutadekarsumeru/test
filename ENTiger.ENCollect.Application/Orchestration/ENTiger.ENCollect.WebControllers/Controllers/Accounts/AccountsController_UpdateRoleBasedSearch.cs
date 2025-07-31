using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/update/role/scope")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateRoleBasedSearch([FromBody]UpdateAccountScopeConfigurationDto dto)
        {
            return await RunService(200, dto, _processAccountsService.UpdateRoleBasedSearch);
        }
    }
}
