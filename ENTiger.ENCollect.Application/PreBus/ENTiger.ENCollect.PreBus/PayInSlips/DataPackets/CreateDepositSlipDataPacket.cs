using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateDepositSlipDataPacket : FlexiFlowDataPacketWithDtoBridge<CreateDepositSlipDto, FlexAppContextBridge>
    {
        protected readonly ILogger<CreateDepositSlipDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public CreateDepositSlipDataPacket(ILogger<CreateDepositSlipDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}