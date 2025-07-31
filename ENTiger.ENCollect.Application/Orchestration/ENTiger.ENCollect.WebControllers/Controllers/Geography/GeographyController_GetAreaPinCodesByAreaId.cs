using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/areaPincodeMappingsByAreaId")]
        [ProducesResponseType(typeof(IEnumerable<GetAreaPinCodesByAreaIdDto>), 200)]
        public async Task<IActionResult> GetAreaPinCodesByAreaId(string areaId)
        {
            GetAreaPinCodesByAreaIdParams parameters = new GetAreaPinCodesByAreaIdParams() { Id = areaId };
            return await RunQueryListServiceAsync<GetAreaPinCodesByAreaIdParams, GetAreaPinCodesByAreaIdDto>(parameters, _processGeographyService.GetAreaPinCodesByAreaId);
        }
    }
}