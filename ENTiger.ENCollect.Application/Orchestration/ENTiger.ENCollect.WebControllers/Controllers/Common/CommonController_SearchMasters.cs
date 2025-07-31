using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/masters/search")]
        [Authorize(Policy = "CanSearchMastersPolicy")]
        [ProducesResponseType(typeof(SearchMastersDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchMasters([FromBody] SearchMastersParams parameters)
        {
            return await RunQuerySingleServiceAsync<SearchMastersParams, SearchMastersDto>(parameters, _processCommonService.SearchMasters);
        }
    }
}