using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.ApplicationUsersModule
{

    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpGet()]
        [Route("mvp/user/wallet/details")]
        [Authorize(Policy = "CanViewWalletDetailsPolicy")]
        [ProducesResponseType(typeof(GetUserWalletDetailsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserWalletDetails([FromQuery]GetUserWalletDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetUserWalletDetailsParams, GetUserWalletDetailsDto>(
                        parameters, _processApplicationUsersService.GetUserWalletDetails);
        }
    }
}
