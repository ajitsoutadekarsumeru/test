using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AcknowledgeCollectionBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<AcknowledgeCollectionBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AcknowledgeCollectionBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AcknowledgeCollectionBatchDataPacket(ILogger<AcknowledgeCollectionBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}