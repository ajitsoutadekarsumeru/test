using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class DevicesController : FlexControllerBridge<DevicesController>
    {
        [HttpPost]
        [Route("device/verify")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> VerifyRegisteredDevice([FromBody] VerifyRegisteredDeviceDto dto)
        {
            return await RunService(201, dto, _processDevicesService.VerifyRegisteredDevice);
        }
    }
}