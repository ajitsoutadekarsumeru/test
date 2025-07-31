using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RenewAgencyDataPacket : FlexiFlowDataPacketWithDtoBridge<RenewAgencyDto, FlexAppContextBridge>
    {
        protected readonly ILogger<RenewAgencyDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public RenewAgencyDataPacket(ILogger<RenewAgencyDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}