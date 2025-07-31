using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class RequestCustomerConsentDataPacket : FlexiFlowDataPacketWithDtoBridge<RequestCustomerConsentDto, FlexAppContextBridge>
    {

        protected readonly ILogger<RequestCustomerConsentDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RequestCustomerConsentDataPacket(ILogger<RequestCustomerConsentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        public string MobileNumber { get; set; }
        public string EmailId { get; set; }

        #endregion
    }
}
