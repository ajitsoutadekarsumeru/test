using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{

    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("allocation/achieved/file/generate")]
        [ProducesResponseType(typeof(GenerateAllocationAchievedFileDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateAllocationAchievedFile([FromBody]GenerateAllocationAchievedFileParams parameters)
        {
            return RunQuerySingleService<GenerateAllocationAchievedFileParams, GenerateAllocationAchievedFileDto>(
                        parameters, _processAllocationService.GenerateAllocationAchievedFile);
        }
    }
}
