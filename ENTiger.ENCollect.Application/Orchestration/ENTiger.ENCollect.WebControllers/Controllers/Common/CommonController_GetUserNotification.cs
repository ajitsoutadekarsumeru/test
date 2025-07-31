using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommonModule
{

    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("user/notification")]
        [ProducesResponseType(typeof(GetUserNotificationDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserNotification([FromQuery]GetUserNotificationParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetUserNotificationParams, GetUserNotificationDto>(
                        parameters, _processCommonService.GetUserNotification);
        }
    }
}
