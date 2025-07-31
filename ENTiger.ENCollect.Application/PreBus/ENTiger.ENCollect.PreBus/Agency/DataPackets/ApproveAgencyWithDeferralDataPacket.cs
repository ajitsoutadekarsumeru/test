using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveAgencyWithDeferralDataPacket : FlexiFlowDataPacketWithDtoBridge<ApproveAgencyWithDeferralDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ApproveAgencyWithDeferralDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ApproveAgencyWithDeferralDataPacket(ILogger<ApproveAgencyWithDeferralDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}