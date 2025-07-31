using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet()]
        [Route("mvp/bucketmaster")]
        [ProducesResponseType(typeof(IEnumerable<GetBucketListDto>), 200)]
        public async Task<IActionResult> GetBucketList([FromQuery] GetBucketListParams parameters)
        {
            return await RunQueryListServiceAsync<GetBucketListParams, GetBucketListDto>(
                        parameters, _processCommonService.GetBucketList);
        }
    }
}