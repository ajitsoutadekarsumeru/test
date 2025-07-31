using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreatePayInSlipDataPacket : FlexiFlowDataPacketWithDtoBridge<CreatePayInSlipDto, FlexAppContextBridge>
    {
        protected readonly ILogger<CreatePayInSlipDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public CreatePayInSlipDataPacket(ILogger<CreatePayInSlipDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}