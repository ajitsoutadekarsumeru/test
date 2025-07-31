using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.DesignationsModule
{
    public partial class DesignationsController : FlexControllerBridge<DesignationsController>
    {
        [HttpGet()]
        [Route("designations/scheme/details")]
        [Authorize(Policy = "CanViewDesignationSchemeDetailsPolicy")]
        [ProducesResponseType(typeof(IEnumerable<GetDesignationsWithSchemeDetailsDto>), 200)]
        public async Task<IActionResult> GetDesignationsWithSchemeDetails([FromQuery] GetDesignationsWithSchemeDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetDesignationsWithSchemeDetailsParams, GetDesignationsWithSchemeDetailsDto>(
                        parameters, _processDesignationsService.GetDesignationsWithSchemeDetails);
        }
    }
}
