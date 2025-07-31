using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/users/create/batch/status")]
        [ProducesResponseType(typeof(IEnumerable<SearchCreateUsersByBatchStatusDto>), 200)]
        public async Task<IActionResult> SearchCreateUsersByBatchStatus([FromBody] SearchCreateUsersByBatchStatusParams parameters)
        {
            return await RunQueryListServiceAsync<SearchCreateUsersByBatchStatusParams, SearchCreateUsersByBatchStatusDto>(
                        parameters, _processCommonService.SearchCreateUsersByBatchStatus);
        }
    }
}