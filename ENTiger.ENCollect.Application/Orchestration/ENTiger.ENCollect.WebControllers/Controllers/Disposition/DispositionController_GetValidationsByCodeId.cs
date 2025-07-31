using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DispositionModule
{
    public partial class DispositionController : FlexControllerBridge<DispositionController>
    {
        [HttpPost()]
        [Route("dispositionValidationmaster")]
        [ProducesResponseType(typeof(IEnumerable<GetValidationsByCodeIdDto>), 200)]
        public async Task<IActionResult> GetValidationsByCodeId([FromBody] GetValidationsByCodeIdParams parameters)
        {
            return await RunQueryListServiceAsync<GetValidationsByCodeIdParams, GetValidationsByCodeIdDto>(
                        parameters, _processDispositionService.GetValidationsByCodeId);
        }
    }
}