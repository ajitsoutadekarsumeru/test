using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpGet()]
        [Route("get/designation")]
        [ProducesResponseType(typeof(IEnumerable<GetDesignationsDto>), 200)]
        public async Task<IActionResult> GetDesignations([FromQuery] GetDesignationsParams parameters)
        {
            return await RunQueryListServiceAsync<GetDesignationsParams, GetDesignationsDto>(
                        parameters, _processDesignationsService.GetDesignations);
        }
    }
}