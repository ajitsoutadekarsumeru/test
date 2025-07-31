using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DepartmentsModule
{
    public partial class DepartmentsController : FlexControllerBridge<DepartmentsController>
    {
        [HttpGet()]
        [Route("search/DepartmentMaster")]
        [ProducesResponseType(typeof(IEnumerable<SearchDepartmentDto>), 200)]
        public async Task<IActionResult> SearchDepartment(string search)
        {
            SearchDepartmentParams parameters = new SearchDepartmentParams();
            parameters.search = search;

            return await RunQueryListServiceAsync<SearchDepartmentParams, SearchDepartmentDto>(
                        parameters, _processDepartmentsService.SearchDepartment);
        }
    }
}