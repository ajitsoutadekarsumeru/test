using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule;


public partial class AllocationController : FlexControllerBridge<AllocationController>
{
    [HttpPost()]
    [Route("secondary/allocation/details")]
    [ProducesResponseType(typeof(FlexiPagedList<GetSecondaryAllocationDetailsDto>), 200)]
    public async Task<IActionResult> GetSecondaryAllocationDetails([FromBody]GetSecondaryAllocationDetailsParams parameters)
    {
        return await RunQueryPagedServiceAsync<GetSecondaryAllocationDetailsParams, GetSecondaryAllocationDetailsDto>(parameters, _processAllocationService.GetSecondaryAllocationDetails);
    }

}


