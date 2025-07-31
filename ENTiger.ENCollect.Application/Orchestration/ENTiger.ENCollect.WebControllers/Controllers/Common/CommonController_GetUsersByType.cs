using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost()]
        [Route("mvp/GetCompanyAndAgencyUser")]
        [ProducesResponseType(typeof(IEnumerable<GetUsersByTypeDto>), 200)]
        public async Task<IActionResult> GetUsersByType([FromBody] GetUsersByTypeParams parameters)
        {
            return await RunQueryListServiceAsync<GetUsersByTypeParams, GetUsersByTypeDto>(
                        parameters, _processCommonService.GetUsersByType);
        }
    }
}