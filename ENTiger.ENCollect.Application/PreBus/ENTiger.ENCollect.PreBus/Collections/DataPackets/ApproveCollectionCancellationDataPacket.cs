using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveCollectionCancellationDataPacket : FlexiFlowDataPacketWithDtoBridge<ApproveCollectionCancellationDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ApproveCollectionCancellationDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ApproveCollectionCancellationDataPacket(ILogger<ApproveCollectionCancellationDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}