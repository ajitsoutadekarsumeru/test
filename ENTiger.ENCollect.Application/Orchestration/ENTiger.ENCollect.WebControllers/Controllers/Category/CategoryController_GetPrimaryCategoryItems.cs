using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class CategoryController : FlexControllerBridge<CategoryController>
    {
        [HttpGet()]
        [Route("get/primaryCategoryItems")]
        [ProducesResponseType(typeof(IEnumerable<GetPrimaryCategoryItemsDto>), 200)]
        public async Task<IActionResult> GetPrimaryCategoryItems(string categoryMasterId)
        {
            GetPrimaryCategoryItemsParams parameters = new GetPrimaryCategoryItemsParams();
            parameters.CategoryMasterId = categoryMasterId;
            return await RunQueryListServiceAsync<GetPrimaryCategoryItemsParams, GetPrimaryCategoryItemsDto>(parameters, _processCategoryService.GetPrimaryCategoryItems);
        }
    }
}