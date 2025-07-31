using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    public partial class UserSearchCriteriaController : FlexControllerBridge<UserSearchCriteriaController>
    {
        [HttpGet()]
        [Route("getallfilters")]
        [ProducesResponseType(typeof(IEnumerable<USGetListDto>), 200)]
        public async Task<IActionResult> GetList([FromQuery] GetListParams parameters)
        {
            return await RunQueryListServiceAsync<GetListParams, USGetListDto>(
                        parameters, _processUserSearchCriteriaService.GetList);
        }
    }
}