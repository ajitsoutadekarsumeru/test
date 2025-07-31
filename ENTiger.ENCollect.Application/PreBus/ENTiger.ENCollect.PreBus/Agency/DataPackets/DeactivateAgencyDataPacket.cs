using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeactivateAgencyDataPacket : FlexiFlowDataPacketWithDtoBridge<DeactivateAgencyDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DeactivateAgencyDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DeactivateAgencyDataPacket(ILogger<DeactivateAgencyDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}