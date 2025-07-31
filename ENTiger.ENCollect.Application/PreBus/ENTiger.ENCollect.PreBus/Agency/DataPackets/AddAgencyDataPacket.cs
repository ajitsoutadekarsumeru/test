using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddAgencyDataPacket : FlexiFlowDataPacketWithDtoBridge<AddAgencyDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddAgencyDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddAgencyDataPacket(ILogger<AddAgencyDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}