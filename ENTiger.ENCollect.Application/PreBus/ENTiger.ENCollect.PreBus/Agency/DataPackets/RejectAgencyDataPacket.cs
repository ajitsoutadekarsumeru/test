using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectAgencyDataPacket : FlexiFlowDataPacketWithDtoBridge<RejectAgencyDto, FlexAppContextBridge>
    {
        protected readonly ILogger<RejectAgencyDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public RejectAgencyDataPacket(ILogger<RejectAgencyDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}