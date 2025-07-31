using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{

    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("moneymovement/file/generate")]
        [ProducesResponseType(typeof(GenerateMoneyMovementFileDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateMoneyMovementFile([FromBody]GenerateMoneyMovementFileParams parameters)
        {
            return RunQuerySingleService<GenerateMoneyMovementFileParams, GenerateMoneyMovementFileDto>(
                        parameters, _processCollectionsService.GenerateMoneyMovementFile);
        }
    }
}
