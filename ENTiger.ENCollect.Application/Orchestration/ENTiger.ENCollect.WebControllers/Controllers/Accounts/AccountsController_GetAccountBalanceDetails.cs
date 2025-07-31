using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/balance/details")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> GetAccountBalanceDetails([FromBody]GetAccountBalanceDetailsDto dto)
        {
            return await RunService(201, dto, _processAccountsService.GetAccountBalanceDetails);
        }
    }
}
