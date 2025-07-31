using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost]
        [Route("collection/BulkUpload")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> CollectionBulkUpload([FromBody]CollectionBulkUploadDto dto)
        {
            dto.CustomId = DateTime.Now.ToString("MMddyyyyhhmmssfff");
            return await RunService(201, dto, _processCollectionsService.CollectionBulkUpload);
        }
    }
}
