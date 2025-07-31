using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpPost]
        //[Authorize(Policy = "CanUpdateSettlementStatusPolicy")]
        [Route("update/status")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateStatus([FromBody]UpdateStatusDto dto)
        {
            return await RunService(200, dto, _processSettlementService.UpdateStatus);
        }
    }
}
