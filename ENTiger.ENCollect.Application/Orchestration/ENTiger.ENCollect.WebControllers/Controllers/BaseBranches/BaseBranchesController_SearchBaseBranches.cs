using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.BaseBranchesModule
{
    public partial class BaseBranchesController : FlexControllerBridge<BaseBranchesController>
    {
        [HttpGet()]
        [Route("search/BaseBranchMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchBaseBranchDto>), 200)]
        public async Task<IActionResult> SearchBaseBranches(string search)
        {
            SearchBaseBranchParams parameters = new SearchBaseBranchParams();
            parameters.Query = search;

            return await RunQueryListServiceAsync<SearchBaseBranchParams, SearchBaseBranchDto>(
                        parameters, _processBaseBranchesService.SearchBaseBranches);
        }
    }
}