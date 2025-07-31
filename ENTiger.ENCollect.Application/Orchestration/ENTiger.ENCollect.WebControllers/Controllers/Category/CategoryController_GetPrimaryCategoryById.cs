using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class CategoryController : FlexControllerBridge<CategoryController>
    {
        [HttpGet()]
        [Route("get/primaryCategoryItem")]
        [ProducesResponseType(typeof(GetPrimaryCategoryByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPrimaryCategoryById(string id)
        {
            GetPrimaryCategoryByIdParams parameters = new GetPrimaryCategoryByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetPrimaryCategoryByIdParams, GetPrimaryCategoryByIdDto>(
                        parameters, _processCategoryService.GetPrimaryCategoryById);
        }
    }
}