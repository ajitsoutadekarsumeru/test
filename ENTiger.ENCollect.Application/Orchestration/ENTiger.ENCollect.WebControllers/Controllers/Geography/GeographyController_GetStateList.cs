using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.GeographyModule
{
    public partial class GeographyController : FlexControllerBridge<GeographyController>
    {
        [HttpGet()]
        [Route("get/states")]
        [ProducesResponseType(typeof(IEnumerable<GetStateListDto>), 200)]
        public async Task<IActionResult> GetStateList([FromBody] GetStateListParams parameters)
        {
            return await RunQueryListServiceAsync<GetStateListParams, GetStateListDto>(
                        parameters, _processGeographyService.GetStateList);
        }
    }
}