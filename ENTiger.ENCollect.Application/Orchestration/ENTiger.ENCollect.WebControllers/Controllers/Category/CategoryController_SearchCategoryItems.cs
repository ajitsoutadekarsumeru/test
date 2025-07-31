using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class CategoryController : FlexControllerBridge<CategoryController>
    {
        [HttpGet()]
        [Route("search/CategoryItems")]
        [ProducesResponseType(typeof(IEnumerable<SearchCategoryItemsDto>), 200)]
        public async Task<IActionResult> SearchCategoryItems(string search, string masterId)
        {
            SearchCategoryItemsParams parameters = new SearchCategoryItemsParams();
            parameters.SearchParam = search;
            parameters.MasterId = masterId;
            return await RunQueryListServiceAsync<SearchCategoryItemsParams, SearchCategoryItemsDto>(
                        parameters, _processCategoryService.SearchCategoryItems);
        }
    }
}