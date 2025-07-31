using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateCollectionBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateCollectionBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateCollectionBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCollectionBatchDataPacket(ILogger<UpdateCollectionBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}