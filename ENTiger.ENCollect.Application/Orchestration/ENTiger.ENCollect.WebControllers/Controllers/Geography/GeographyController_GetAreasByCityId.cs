using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/AreaMasterBycity")]
        [ProducesResponseType(typeof(IEnumerable<GetAreasByCityIdDto>), 200)]
        public async Task<IActionResult> GetAreasByCityId(string cityId)
        {
            GetAreasByCityIdParams parameters = new GetAreasByCityIdParams() { Id = cityId };
            return await RunQueryListServiceAsync<GetAreasByCityIdParams, GetAreasByCityIdDto>(parameters, _processGeographyService.GetAreasByCityId);
        }
    }
}