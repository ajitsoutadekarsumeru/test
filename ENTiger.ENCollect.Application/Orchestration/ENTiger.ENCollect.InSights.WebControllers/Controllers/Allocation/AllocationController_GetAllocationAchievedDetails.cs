using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule
{

    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("allocation/achieved/details")]
        [ProducesResponseType(typeof(FlexiPagedList<GetAllocationAchievedDetailsDto>), 200)]
        public async Task<IActionResult> GetAllocationAchievedDetails([FromBody] GetAllocationAchievedDetailsParams parameters)
        {
            return await RunQueryPagedServiceAsync<GetAllocationAchievedDetailsParams, GetAllocationAchievedDetailsDto>(parameters, _processAllocationService.GetAllocationAchievedDetails);
        }


    }


}
