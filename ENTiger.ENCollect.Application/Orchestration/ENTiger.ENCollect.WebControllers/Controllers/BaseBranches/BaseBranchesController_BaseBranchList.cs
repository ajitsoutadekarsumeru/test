using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    public partial class BaseBranchesController : FlexControllerBridge<BaseBranchesController>
    {
        [HttpGet()]
        [Route("get/basebranches")]
        [ProducesResponseType(typeof(IEnumerable<BaseBranchListDto>), 200)]
        public async Task<IActionResult> BaseBranchList([FromQuery] BaseBranchListParams parameters)
        {
            return await RunQueryListServiceAsync<BaseBranchListParams, BaseBranchListDto>(
                        parameters, _processBaseBranchesService.BaseBranchList);
        }
    }
}