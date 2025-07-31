using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountsController : FlexControllerBridge<AccountsController>
    {
        [HttpPost]
        [Route("account/getByCustomerID")]
        [ProducesResponseType(typeof(IEnumerable<GetByCustomerIdDto>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCustomerId([FromBody] GetByCustomerIdParams parameters)
        {
            return await RunQueryListServiceAsync<GetByCustomerIdParams, GetByCustomerIdDto>(parameters, _processAccountsService.GetByCustomerId);
        }
    }
}