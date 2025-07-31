using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("add/physical/collection")]
        [Authorize(Policy = "CanCreateWalkinReceiptPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddPhysicalCollection([FromBody] AddPhysicalCollectionDto dto)
        {
            var result = RateLimit(dto, "add_physical_collection");
            return result ?? await RunService(201, dto, _processCollectionsService.AddPhysicalCollection);
        }
    }
}