using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    public partial class BaseBranchesController : FlexControllerBridge<BaseBranchesController>
    {
        [HttpGet()]
        [Route("get/basebranch/{id}")]
        [ProducesResponseType(typeof(BaseBranchByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BaseBranchById(string id)
        {
            BaseBranchByIdParams parameters = new BaseBranchByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<BaseBranchByIdParams, BaseBranchByIdDto>(
                        parameters, _processBaseBranchesService.BaseBranchById);
        }
    }
}