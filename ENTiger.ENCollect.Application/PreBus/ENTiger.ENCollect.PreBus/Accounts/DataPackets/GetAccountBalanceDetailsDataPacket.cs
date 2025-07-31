using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAccountBalanceDetailsDataPacket : FlexiFlowDataPacketWithDtoBridge<GetAccountBalanceDetailsDto, FlexAppContextBridge>
    {

        protected readonly ILogger<GetAccountBalanceDetailsDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetAccountBalanceDetailsDataPacket(ILogger<GetAccountBalanceDetailsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public GetAccountBalanceOutputDetailsDto getAccountBalanceDetailsDto { get; set; }

        #endregion
    }
}
