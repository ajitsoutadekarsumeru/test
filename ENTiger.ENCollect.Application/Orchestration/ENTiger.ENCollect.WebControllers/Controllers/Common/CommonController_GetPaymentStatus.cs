using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/paymentStatus")]
        [ProducesResponseType(typeof(IEnumerable<GetPaymentStatusDto>), 200)]
        public async Task<IActionResult> GetPaymentStatus([FromQuery] GetPaymentStatusParams parameters)
        {
            return await RunQueryListServiceAsync<GetPaymentStatusParams, GetPaymentStatusDto>(
                        parameters, _processCommonService.GetPaymentStatus);
        }
    }
}