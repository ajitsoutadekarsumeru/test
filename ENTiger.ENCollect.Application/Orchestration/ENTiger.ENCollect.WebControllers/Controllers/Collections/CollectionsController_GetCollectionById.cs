using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpGet()]
        [Route("payment/receipt/view/{id}")]
        [ProducesResponseType(typeof(GetCollectionByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCollectionById(string id)
        {
            GetCollectionByIdParams parameters = new GetCollectionByIdParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<GetCollectionByIdParams, GetCollectionByIdDto>(
                        parameters, _processCollectionsService.GetCollectionById);
        }
    }
}