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
        [Route("collection/getfile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> GetCollectionFile([FromBody]GetCollectionFileDto dto)
        {
            var result = await RunService(200, dto, _processCollectionsService.GetCollectionFile);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName, (result as ObjectResult).Value.ToString());
            }
            return result;
        }
    }
}
