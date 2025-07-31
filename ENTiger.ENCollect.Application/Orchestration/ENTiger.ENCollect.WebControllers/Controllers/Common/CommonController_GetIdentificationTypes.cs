using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/IdentificationTypes")]
        [ProducesResponseType(typeof(IEnumerable<GetIdentificationTypesDto>), 200)]
        public async Task<IActionResult> GetIdentificationTypes([FromQuery] GetIdentificationTypesParams parameters)
        {
            return await RunQueryListServiceAsync<GetIdentificationTypesParams, GetIdentificationTypesDto>(
                        parameters, _processCommonService.GetIdentificationTypes);
        }
    }
}