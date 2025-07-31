using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("segment/all/segmentloanaccountscount/{id}")]
        [ProducesResponseType(typeof(GetSegmentAccountsCountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSegmentAccountsCount(string id)
        {
            GetSegmentAccountsCountParams parameters = new GetSegmentAccountsCountParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetSegmentAccountsCountParams, GetSegmentAccountsCountDto>(
                        parameters, _processSegmentationService.GetSegmentAccountsCount);
        }
    }
}