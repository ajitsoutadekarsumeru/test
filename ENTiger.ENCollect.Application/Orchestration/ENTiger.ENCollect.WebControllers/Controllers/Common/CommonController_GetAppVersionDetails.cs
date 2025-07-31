using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/device/deviceversioncheck")]
        [ProducesResponseType(typeof(GetAppVersionDetailsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAppVersionDetails([FromBody] GetAppVersionDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetAppVersionDetailsParams, GetAppVersionDetailsDto>(
                        parameters, _processCommonService.GetAppVersionDetails);
        }
    }
}