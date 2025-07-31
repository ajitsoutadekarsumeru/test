using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/getcolor")]
        [ProducesResponseType(typeof(GetColorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetColor()
        {
            GetColorParams parameters = new GetColorParams();
            return await RunQuerySingleServiceAsync<GetColorParams, GetColorDto>(parameters, _processCommonService.GetColor);
        }
    }
}