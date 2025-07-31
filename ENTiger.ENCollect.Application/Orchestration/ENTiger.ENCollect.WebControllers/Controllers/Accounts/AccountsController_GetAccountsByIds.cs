using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{

    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost()]
        [Route("account/batch")]
        [ProducesResponseType(typeof(IEnumerable<GetAccountsByIdsDto>), 200)]
        public async Task<IActionResult> GetAccountsByIds([FromBody] GetAccountsByIdsParams parameters)
        {
            return await RunQueryListServiceAsync<GetAccountsByIdsParams, GetAccountsByIdsDto>(parameters, _processAccountsService.GetAccountsByIds);
        }
    }
}
