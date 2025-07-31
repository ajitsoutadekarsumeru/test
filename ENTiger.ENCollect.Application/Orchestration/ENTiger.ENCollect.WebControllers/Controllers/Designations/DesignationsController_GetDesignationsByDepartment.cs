using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpGet()]
        [Route("get/designations")]
        [ProducesResponseType(typeof(IEnumerable<GetDesignationsByDepartmentDto>), 200)]
        public async Task<IActionResult> GetDesignationsByDepartment(string? departmentId)
        {
            GetDesignationsByDepartmentParams parameters = new GetDesignationsByDepartmentParams() { DepartmentId = departmentId };
            return await RunQueryListServiceAsync<GetDesignationsByDepartmentParams, GetDesignationsByDepartmentDto>(parameters, _processDesignationsService.GetDesignationsByDepartment);
        }
    }
}