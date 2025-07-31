using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateLoanAccountFlagDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateLoanAccountFlagDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateLoanAccountFlagDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountFlagDataPacket(ILogger<UpdateLoanAccountFlagDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}