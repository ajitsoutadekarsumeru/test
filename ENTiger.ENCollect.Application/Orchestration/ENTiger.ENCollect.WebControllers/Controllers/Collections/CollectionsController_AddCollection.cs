using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("add/collection")]
        [Authorize(Policy = "CanCreateReceiptPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddCollection([FromBody] AddCollectionDto dto)
        {
            var result = RateLimit(dto, "add_collection");
            return result ?? await RunService(201, dto, _processCollectionsService.AddCollection);
        }
    }
}