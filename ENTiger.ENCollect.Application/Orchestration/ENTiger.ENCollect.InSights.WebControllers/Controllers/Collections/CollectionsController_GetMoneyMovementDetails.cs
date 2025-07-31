using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{

    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("moneymovement/details")]
        [ProducesResponseType(typeof(FlexiPagedList<GetMoneyMovementDetailsDto>), 200)]
        public async Task<IActionResult> GetMoneyMovementDetails([FromBody]GetMoneyMovementDetailsParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetMoneyMovementDetailsParams, GetMoneyMovementDetailsDto>(parameters, _processCollectionsService.GetMoneyMovementDetails);
        }

    }

    
}
