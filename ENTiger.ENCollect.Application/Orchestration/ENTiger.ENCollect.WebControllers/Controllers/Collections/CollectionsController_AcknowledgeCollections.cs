using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("update/collection")]
        [Authorize(Policy = "CanAcknowledgeReceiptPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> AcknowledgeCollections([FromBody] AcknowledgeCollectionsDto dto)
        {
            return await RunService(200, dto, _processCollectionsService.AcknowledgeCollections);
        }
    }
}