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
        [Authorize(Policy = "CanEditSettlementPolicy")]
        [Route("update")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> UpdateSettlement([FromBody]UpdateSettlementDto dto)
        {
            return await RunService(201, dto, _processSettlementService.UpdateSettlement);
        }
    }
}
