using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateCustomerConsentExpiryDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateCustomerConsentExpiryDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateCustomerConsentExpiryDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCustomerConsentExpiryDataPacket(ILogger<UpdateCustomerConsentExpiryDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
