using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/areaPincodeMappings")]
        [ProducesResponseType(typeof(IEnumerable<GetAreaPinCodesDto>), 200)]
        public async Task<IActionResult> GetAreaPinCodes()
        {
            GetAreaPinCodesParams parameters = new GetAreaPinCodesParams();
            return await RunQueryListServiceAsync<GetAreaPinCodesParams, GetAreaPinCodesDto>(parameters, _processGeographyService.GetAreaPinCodes);
        }
    }
}