using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{

    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("primary/allocation/file/generate")]
        [ProducesResponseType(typeof(GeneratePrimaryAllocationFileDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GeneratePrimaryAllocationFile([FromBody]GeneratePrimaryAllocationFileParams parameters)
        {
            return RunQuerySingleService<GeneratePrimaryAllocationFileParams, GeneratePrimaryAllocationFileDto>(
                        parameters, _processAllocationService.GeneratePrimaryAllocationFile);
        }
    }
}
