using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveCollectionAgencyDataPacket : FlexiFlowDataPacketWithDtoBridge<ApproveCollectionAgencyDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ApproveCollectionAgencyDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ApproveCollectionAgencyDataPacket(ILogger<ApproveCollectionAgencyDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}