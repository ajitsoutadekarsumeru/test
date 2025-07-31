using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpGet()]
        [Route("get/ReportToDesignations")]
        [ProducesResponseType(typeof(IEnumerable<GetDesignationsByLevelDto>), 200)]
        public async Task<IActionResult> GetDesignationsByLevel(int level)
        {
            GetDesignationsByLevelParams parameters = new GetDesignationsByLevelParams();
            parameters.level = level;
            return await RunQueryListServiceAsync<GetDesignationsByLevelParams, GetDesignationsByLevelDto>(
                        parameters, _processDesignationsService.GetDesignationsByLevel);
        }
    }
}