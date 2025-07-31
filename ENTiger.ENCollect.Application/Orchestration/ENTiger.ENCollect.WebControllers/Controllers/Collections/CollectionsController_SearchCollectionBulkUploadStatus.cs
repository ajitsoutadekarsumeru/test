using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{

    public partial class CollectionsController : FlexControllerBridge<CollectionsController>
    {
        [HttpPost()]
        [Route("collection/UploadStatus")]
        [ProducesResponseType(typeof(FlexiPagedList<SearchCollectionBulkUploadStatusDto>), 200)]
        public async Task<IActionResult> SearchCollectionBulkUploadStatus([FromBody] SearchCollectionBulkUploadStatusParams parameters)
        {
            return await RunQueryPagedServiceAsync<SearchCollectionBulkUploadStatusParams, SearchCollectionBulkUploadStatusDto>(parameters, _processCollectionsService.SearchCollectionBulkUploadStatus);
        }

    }

    
}
