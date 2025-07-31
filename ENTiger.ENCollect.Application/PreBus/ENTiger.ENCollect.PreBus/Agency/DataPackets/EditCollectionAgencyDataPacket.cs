using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class EditCollectionAgencyDataPacket : FlexiFlowDataPacketWithDtoBridge<EditCollectionAgencyDto, FlexAppContextBridge>
    {
        protected readonly ILogger<EditCollectionAgencyDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public EditCollectionAgencyDataPacket(ILogger<EditCollectionAgencyDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}