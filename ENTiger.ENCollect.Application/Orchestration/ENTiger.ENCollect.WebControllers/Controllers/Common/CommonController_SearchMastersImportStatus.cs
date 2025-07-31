using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/masters/import/status")]
        [Authorize(Policy = "CanSearchUploadMastersStatusPolicy")]
        [ProducesResponseType(typeof(IEnumerable<SearchMastersImportStatusDto>), 200)]
        public async Task<IActionResult> SearchMastersImportStatus([FromBody] SearchMastersImportStatusParams parameters)
        {
            return await RunQueryListServiceAsync<SearchMastersImportStatusParams, SearchMastersImportStatusDto>(parameters, _processCommonService.SearchMastersImportStatus);
        }
    }
}