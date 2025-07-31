using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/Master/Userpersona")]
        [ProducesResponseType(typeof(IEnumerable<GetUserPersonasDto>), 200)]
        public async Task<IActionResult> GetUserPersonas([FromQuery] GetUserPersonasParams parameters)
        {
            return await RunQueryListServiceAsync<GetUserPersonasParams, GetUserPersonasDto>(
                        parameters, _processCommonService.GetUserPersonas);
        }
    }
}