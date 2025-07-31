using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DepartmentsModule
{
    public partial class DepartmentsController : FlexControllerBridge<DepartmentsController>
    {
        [HttpGet()]
        [Route("get/departments")]
        [ProducesResponseType(typeof(IEnumerable<GetDepartmentsListDto>), 200)]
        public async Task<IActionResult> GetDepartmentsList([FromQuery] GetDepartmentsListParams parameters)
        {
            return await RunQueryListServiceAsync<GetDepartmentsListParams, GetDepartmentsListDto>(
                        parameters, _processDepartmentsService.GetDepartmentsList);
        }
    }
}