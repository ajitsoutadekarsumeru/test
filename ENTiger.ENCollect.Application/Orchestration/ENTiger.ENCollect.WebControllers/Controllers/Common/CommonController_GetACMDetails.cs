using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/master/details/{IsMobiledevice}")]
        [ProducesResponseType(typeof(IEnumerable<GetACMDetailsDto>), 200)]
        public async Task<IActionResult> GetACMDetails(bool IsMobiledevice)
        {
            GetACMDetailsParams parameters = new GetACMDetailsParams();
            parameters.IsMobile = IsMobiledevice;
            return await RunQueryListServiceAsync<GetACMDetailsParams, GetACMDetailsDto>(parameters, _processCommonService.GetACMDetails);
        }
    }
}