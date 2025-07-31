using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/state/{id}")]
        [ProducesResponseType(typeof(GetStateByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStateById(string id)
        {
            GetStateByIdParams parameters = new GetStateByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetStateByIdParams, GetStateByIdDto>(
                        parameters, _processGeographyService.GetStateById);
        }
    }
}