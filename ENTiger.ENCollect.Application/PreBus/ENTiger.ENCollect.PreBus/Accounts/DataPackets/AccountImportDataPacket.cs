using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AccountImportDataPacket : FlexiFlowDataPacketWithDtoBridge<AccountImportDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AccountImportDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AccountImportDataPacket(ILogger<AccountImportDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}