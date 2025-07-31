using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/city/{id}")]
        [ProducesResponseType(typeof(GetCityByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCityById(string id)
        {
            GetCityByIdParams parameters = new GetCityByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetCityByIdParams, GetCityByIdDto>(
                        parameters, _processGeographyService.GetCityById);
        }
    }
}