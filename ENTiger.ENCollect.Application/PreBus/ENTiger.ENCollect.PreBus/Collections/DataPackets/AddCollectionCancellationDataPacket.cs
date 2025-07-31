using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCollectionCancellationDataPacket : FlexiFlowDataPacketWithDtoBridge<AddCollectionCancellationDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddCollectionCancellationDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddCollectionCancellationDataPacket(ILogger<AddCollectionCancellationDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}