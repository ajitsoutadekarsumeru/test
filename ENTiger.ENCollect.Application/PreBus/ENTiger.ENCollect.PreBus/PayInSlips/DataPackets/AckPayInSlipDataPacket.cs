using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AckPayInSlipDataPacket : FlexiFlowDataPacketWithDtoBridge<AckPayInSlipDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AckPayInSlipDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AckPayInSlipDataPacket(ILogger<AckPayInSlipDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}