using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddPhysicalCollectionDataPacket : FlexiFlowDataPacketWithDtoBridge<AddPhysicalCollectionDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddPhysicalCollectionDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddPhysicalCollectionDataPacket(ILogger<AddPhysicalCollectionDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}