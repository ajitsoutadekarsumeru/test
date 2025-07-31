using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/getGender")]
        [ProducesResponseType(typeof(IEnumerable<GetGendersDto>), 200)]
        public async Task<IActionResult> GetGenders([FromQuery] GetGendersParams parameters)
        {
            return await RunQueryListServiceAsync<GetGendersParams, GetGendersDto>(
                        parameters, _processCommonService.GetGenders);
        }
    }
}