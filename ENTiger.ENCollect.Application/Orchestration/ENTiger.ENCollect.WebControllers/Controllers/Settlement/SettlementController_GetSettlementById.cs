using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("view/{id}")]
        [ProducesResponseType(typeof(GetSettlementByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSettlementById(string id)
        {
            GetSettlementByIdParams parameters = new GetSettlementByIdParams();
            parameters.Id = id;

            return RunQuerySingleService<GetSettlementByIdParams, GetSettlementByIdDto>(
                        parameters, _processSettlementService.GetSettlementById);
        }
    }
}
