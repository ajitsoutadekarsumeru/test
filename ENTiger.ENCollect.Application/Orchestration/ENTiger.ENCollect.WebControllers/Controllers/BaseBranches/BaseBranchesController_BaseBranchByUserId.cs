using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    public partial class BaseBranchesController : FlexControllerBridge<BaseBranchesController>
    {
        [HttpGet()]
        [Route("get/basebranchesbyuserid")]
        [ProducesResponseType(typeof(IEnumerable<BaseBranchByUserIdDto>), 200)]
        public async Task<IActionResult> BaseBranchByUserId([FromQuery] BaseBranchByUserIdParams parameters)
        {
            return await RunQueryListServiceAsync<BaseBranchByUserIdParams, BaseBranchByUserIdDto>(
                        parameters, _processBaseBranchesService.BaseBranchByUserId);
        }
    }
}