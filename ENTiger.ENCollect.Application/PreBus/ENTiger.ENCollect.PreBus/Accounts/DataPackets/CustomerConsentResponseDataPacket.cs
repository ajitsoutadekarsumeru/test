using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class CustomerConsentResponseDataPacket : FlexiFlowDataPacketWithDtoBridge<CustomerConsentResponseDto, FlexAppContextBridge>
    {

        protected readonly ILogger<CustomerConsentResponseDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CustomerConsentResponseDataPacket(ILogger<CustomerConsentResponseDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties
        #endregion
    }
}
