using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpGet()]
        [Route("payment/duepdc")]
        [ProducesResponseType(typeof(IEnumerable<GetDuePDCDetailsDto>), 200)]
        public async Task<IActionResult> GetDuePDCDetails([FromQuery] GetDuePDCDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetDuePDCDetailsParams, GetDuePDCDetailsDto>(
                        parameters, _processCollectionsService.GetDuePDCDetails);
        }
    }
}