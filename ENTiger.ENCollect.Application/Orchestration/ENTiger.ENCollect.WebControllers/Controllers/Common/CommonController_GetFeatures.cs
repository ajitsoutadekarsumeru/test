using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/getFeatures")]
        [ProducesResponseType(typeof(IEnumerable<GetFeaturesDto>), 200)]
        public async Task<IActionResult> GetFeatures([FromQuery] GetFeaturesParams parameters)
        {
            return await RunQueryListServiceAsync<GetFeaturesParams, GetFeaturesDto>(
                        parameters, _processCommonService.GetFeatures);
        }
    }
}