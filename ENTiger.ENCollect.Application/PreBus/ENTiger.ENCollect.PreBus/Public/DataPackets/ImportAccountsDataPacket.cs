using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ImportAccountsDataPacket : FlexiFlowDataPacketWithDtoBridge<ImportAccountsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ImportAccountsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ImportAccountsDataPacket(ILogger<ImportAccountsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}