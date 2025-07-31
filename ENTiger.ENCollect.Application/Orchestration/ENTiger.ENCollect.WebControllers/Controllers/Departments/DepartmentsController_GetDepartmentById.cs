using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DepartmentsModule
{
    public partial class DepartmentsController : FlexControllerBridge<DepartmentsController>
    {
        [HttpGet()]
        [Route("get/department/{id}")]
        [ProducesResponseType(typeof(GetDepartmentByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDepartmentById(string id)
        {
            GetDepartmentByIdParams parameters = new GetDepartmentByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetDepartmentByIdParams, GetDepartmentByIdDto>(
                        parameters, _processDepartmentsService.GetDepartmentById);
        }
    }
}