using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/acm/search")]
        [ProducesResponseType(typeof(IEnumerable<SearchACMDto>), 200)]
        public async Task<IActionResult> SearchACM([FromBody] SearchACMParams parameters)
        {
            return await RunQueryListServiceAsync<SearchACMParams, SearchACMDto>(parameters, _processCommonService.SearchACM);
        }
    }
}