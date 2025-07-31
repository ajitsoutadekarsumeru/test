using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class CategoryController : FlexControllerBridge<CategoryController>
    {
        [HttpGet()]
        [Route("get/secondaryCategoryItemById")]
        [ProducesResponseType(typeof(GetSecondaryCategoryByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSecondaryCategoryById(string id)
        {
            GetSecondaryCategoryByIdParams parameters = new GetSecondaryCategoryByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetSecondaryCategoryByIdParams, GetSecondaryCategoryByIdDto>(
                        parameters, _processCategoryService.GetSecondaryCategoryById);
        }
    }
}