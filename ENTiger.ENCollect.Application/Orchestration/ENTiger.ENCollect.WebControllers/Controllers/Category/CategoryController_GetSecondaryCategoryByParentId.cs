using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class CategoryController : FlexControllerBridge<CategoryController>
    {
        [HttpGet()]
        [Route("get/secondaryCategoryItemByParentId")]
        [ProducesResponseType(typeof(IEnumerable<GetSecondaryCategoryByParentIdDto>), 200)]
        public async Task<IActionResult> GetSecondaryCategoryByParentId(string parentId)
        {
            GetSecondaryCategoryByParentIdParams parameters = new GetSecondaryCategoryByParentIdParams();
            parameters.ParentId = parentId;
            return await RunQueryListServiceAsync<GetSecondaryCategoryByParentIdParams, GetSecondaryCategoryByParentIdDto>(
                        parameters, _processCategoryService.GetSecondaryCategoryByParentId);
        }
    }
}