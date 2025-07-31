using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddLoanAccountFlagDataPacket : FlexiFlowDataPacketWithDtoBridge<AddLoanAccountFlagDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddLoanAccountFlagDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddLoanAccountFlagDataPacket(ILogger<AddLoanAccountFlagDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}