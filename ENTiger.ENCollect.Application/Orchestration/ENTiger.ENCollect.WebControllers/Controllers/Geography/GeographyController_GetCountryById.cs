using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/country/{id}")]
        [ProducesResponseType(typeof(GetCountryByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountryById(string id)
        {
            GetCountryByIdParams parameters = new GetCountryByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetCountryByIdParams, GetCountryByIdDto>(
                        parameters, _processGeographyService.GetCountryById);
        }
    }
}