using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountsForSecondaryAllocationDataPacket : FlexiFlowDataPacketWithDtoBridge<SearchAccountsForSecondaryAllocationDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchAccountsForSecondaryAllocationDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SearchAccountsForSecondaryAllocationDataPacket(ILogger<SearchAccountsForSecondaryAllocationDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}