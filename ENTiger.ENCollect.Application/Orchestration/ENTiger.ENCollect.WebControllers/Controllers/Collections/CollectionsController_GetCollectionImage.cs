using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpGet]
        [Route("collection/getimage")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetCollectionImage(string fileName)
        {
            GetCollectionImageDto dto = new GetCollectionImageDto() { FileName = fileName };
            var result = await RunService(200, dto, _processCollectionsService.GetCollectionImage);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
            }
            return result;
        }
    }
}