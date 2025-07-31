using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CollectionBulkUploadDataPacket : FlexiFlowDataPacketWithDtoBridge<CollectionBulkUploadDto, FlexAppContextBridge>
    {

        protected readonly ILogger<CollectionBulkUploadDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CollectionBulkUploadDataPacket(ILogger<CollectionBulkUploadDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
