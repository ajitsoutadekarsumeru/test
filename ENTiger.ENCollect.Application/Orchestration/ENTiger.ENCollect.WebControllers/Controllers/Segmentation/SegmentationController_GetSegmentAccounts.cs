using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class SegmentationController : FlexControllerBridge<SegmentationController>
    {
        [HttpGet()]
        [Route("segment/all/segmentloanaccounts/{id}")]
        [ProducesResponseType(typeof(GetSegmentAccountsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSegmentAccounts(string id)
        {
            GetSegmentAccountsParams parameters = new GetSegmentAccountsParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetSegmentAccountsParams, GetSegmentAccountsDto>(
                        parameters, _processSegmentationService.GetSegmentAccounts);
        }
    }
}