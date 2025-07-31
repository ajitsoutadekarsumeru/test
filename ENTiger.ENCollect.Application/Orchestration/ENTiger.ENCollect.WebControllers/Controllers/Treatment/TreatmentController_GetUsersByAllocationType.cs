using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpGet()]
        [Route("Get/allocationname")]
        [ProducesResponseType(typeof(IEnumerable<GetUsersByAllocationTypeDto>), 200)]
        public async Task<IActionResult> GetUsersByAllocationType(string Type)
        {
            GetUsersByAllocationTypeParams parameters = new GetUsersByAllocationTypeParams() { Type = Type };
            return await RunQueryListServiceAsync<GetUsersByAllocationTypeParams, GetUsersByAllocationTypeDto>(
                        parameters, _processTreatmentService.GetUsersByAllocationType);
        }
    }
}