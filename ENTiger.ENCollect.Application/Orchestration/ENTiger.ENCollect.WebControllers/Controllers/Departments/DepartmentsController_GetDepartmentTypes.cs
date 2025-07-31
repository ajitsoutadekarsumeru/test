using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DepartmentsModule
{
    public partial class DepartmentsController : FlexControllerBridge<DepartmentsController>
    {
        [HttpGet()]
        [Route("get/DepartmentTypeId")]
        [ProducesResponseType(typeof(IEnumerable<GetDepartmentTypesDto>), 200)]
        public async Task<IActionResult> GetDepartmentTypes([FromQuery] GetDepartmentTypesParams parameters)
        {
            return await RunQueryListServiceAsync<GetDepartmentTypesParams, GetDepartmentTypesDto>(
                        parameters, _processDepartmentsService.GetDepartmentTypes);
        }
    }
}