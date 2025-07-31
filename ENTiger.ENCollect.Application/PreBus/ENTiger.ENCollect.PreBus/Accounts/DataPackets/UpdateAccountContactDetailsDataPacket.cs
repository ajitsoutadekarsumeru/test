using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateAccountContactDetailsDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateAccountContactDetailsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateAccountContactDetailsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateAccountContactDetailsDataPacket(ILogger<UpdateAccountContactDetailsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}