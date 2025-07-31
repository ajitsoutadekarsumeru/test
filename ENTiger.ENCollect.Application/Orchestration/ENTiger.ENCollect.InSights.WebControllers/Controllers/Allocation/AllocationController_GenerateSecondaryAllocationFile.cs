using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{

    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("secondary/allocation/file/generate")]
        [ProducesResponseType(typeof(GenerateSecondaryAllocationFileDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateSecondaryAllocationFile([FromBody]GenerateSecondaryAllocationFileParams parameters)
        {
            return RunQuerySingleService<GenerateSecondaryAllocationFileParams, GenerateSecondaryAllocationFileDto>(
                        parameters, _processAllocationService.GenerateSecondaryAllocationFile);
        }
    }
}
