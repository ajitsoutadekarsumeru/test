using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectCollectionCancellationDataPacket : FlexiFlowDataPacketWithDtoBridge<RejectCollectionCancellationDto, FlexAppContextBridge>
    {
        protected readonly ILogger<RejectCollectionCancellationDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public RejectCollectionCancellationDataPacket(ILogger<RejectCollectionCancellationDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}