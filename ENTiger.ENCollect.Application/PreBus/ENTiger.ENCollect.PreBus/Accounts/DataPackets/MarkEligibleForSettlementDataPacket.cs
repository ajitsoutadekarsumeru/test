using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MarkEligibleForSettlementDataPacket : FlexiFlowDataPacketWithDtoBridge<MarkEligibleForSettlementDto, FlexAppContextBridge>
    {

        protected readonly ILogger<MarkEligibleForSettlementDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public MarkEligibleForSettlementDataPacket(ILogger<MarkEligibleForSettlementDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
