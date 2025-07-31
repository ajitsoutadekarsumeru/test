using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpGet()]
        [Route("moneymovement/summary")]
        [ProducesResponseType(typeof(IEnumerable<GetMoneyMovementSummaryDto>), 200)]
        public async Task<IActionResult> GetMoneyMovementSummary(string? staffOrAgent)
        {
            GetMoneyMovementSummaryParams parameters = new GetMoneyMovementSummaryParams();
            parameters.staffOrAgent = staffOrAgent;

            return await RunQueryListServiceAsync<GetMoneyMovementSummaryParams, GetMoneyMovementSummaryDto>(
                        parameters, _processCollectionsService.GetMoneyMovementSummary);
        }
    }
}
