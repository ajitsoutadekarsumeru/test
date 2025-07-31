using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class DevicesController : FlexControllerBridge<DevicesController>
    {
        [HttpPost]
        [Route("device/validateotp")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ValidateRegisterDeviceOtp([FromBody] ValidateRegisterDeviceOtpDto dto)
        {
            return await RunService(201, dto, _processDevicesService.ValidateRegisterDeviceOtp);
        }
    }
}