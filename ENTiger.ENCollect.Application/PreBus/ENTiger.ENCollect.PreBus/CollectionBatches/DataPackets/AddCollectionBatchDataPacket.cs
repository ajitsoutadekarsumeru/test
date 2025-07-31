using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCollectionBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<AddCollectionBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddCollectionBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddCollectionBatchDataPacket(ILogger<AddCollectionBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}