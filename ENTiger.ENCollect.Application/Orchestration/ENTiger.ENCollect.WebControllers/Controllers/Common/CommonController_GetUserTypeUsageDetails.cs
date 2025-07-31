using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommonModule
{

    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/usertype/usage/details")]
        [ProducesResponseType(typeof(GetUserTypeUsageDetailsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserTypeUsageDetails([FromQuery]GetUserTypeUsageDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetUserTypeUsageDetailsParams, GetUserTypeUsageDetailsDto>(
                        parameters, _processCommonService.GetUserTypeUsageDetails);
        }
    }
}
