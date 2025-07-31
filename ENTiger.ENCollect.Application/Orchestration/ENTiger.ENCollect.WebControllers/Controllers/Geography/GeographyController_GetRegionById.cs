using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/region/{id}")]
        [ProducesResponseType(typeof(GetRegionByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRegionById(string id)
        {
            GetRegionByIdParams parameters = new GetRegionByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetRegionByIdParams, GetRegionByIdDto>(
                        parameters, _processGeographyService.GetRegionById);
        }
    }
}