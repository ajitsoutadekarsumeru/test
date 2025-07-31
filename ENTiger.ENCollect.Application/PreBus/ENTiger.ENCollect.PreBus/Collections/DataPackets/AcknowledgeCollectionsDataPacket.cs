using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AcknowledgeCollectionsDataPacket : FlexiFlowDataPacketWithDtoBridge<AcknowledgeCollectionsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AcknowledgeCollectionsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AcknowledgeCollectionsDataPacket(ILogger<AcknowledgeCollectionsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}