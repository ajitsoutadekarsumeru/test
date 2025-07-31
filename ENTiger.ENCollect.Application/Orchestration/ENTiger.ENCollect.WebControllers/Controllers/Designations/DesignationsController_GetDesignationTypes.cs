using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpGet()]
        [Route("get/designationsType")]
        [ProducesResponseType(typeof(IEnumerable<GetDesignationTypesDto>), 200)]
        public async Task<IActionResult> GetDesignationTypes([FromQuery] GetDesignationTypesParams parameters)
        {
            return await RunQueryListServiceAsync<GetDesignationTypesParams, GetDesignationTypesDto>(
                        parameters, _processDesignationsService.GetDesignationTypes);
        }
    }
}