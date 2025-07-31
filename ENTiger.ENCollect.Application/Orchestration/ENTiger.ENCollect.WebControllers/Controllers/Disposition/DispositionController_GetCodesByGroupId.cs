using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DispositionModule
{
    public partial class DispositionController : FlexControllerBridge<DispositionController>
    {
        [HttpPost()]
        [Route("dispositionCodemaster")]
        [ProducesResponseType(typeof(IEnumerable<GetCodesByGroupIdDto>), 200)]
        public async Task<IActionResult> GetCodesByGroupId([FromBody] GetCodesByGroupIdParams parameters)
        {
            return await RunQueryListServiceAsync<GetCodesByGroupIdParams, GetCodesByGroupIdDto>(
                        parameters, _processDispositionService.GetCodesByGroupId);
        }
    }
}