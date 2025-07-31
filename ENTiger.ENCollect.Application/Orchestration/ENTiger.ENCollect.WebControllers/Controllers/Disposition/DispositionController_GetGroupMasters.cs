using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DispositionModule
{
    public partial class DispositionController : FlexControllerBridge<DispositionController>
    {
        [HttpGet()]
        [Route("dispositiongroupmaster")]
        [ProducesResponseType(typeof(IEnumerable<GetGroupMastersDto>), 200)]
        public async Task<IActionResult> GetGroupMasters(string? dispositionAccess)
        {
            GetGroupMastersParams parameters = new GetGroupMastersParams();
            parameters.dispositionAccess = dispositionAccess;
            return await RunQueryListServiceAsync<GetGroupMastersParams, GetGroupMastersDto>(
                        parameters, _processDispositionService.GetGroupMasters);
        }
    }
}