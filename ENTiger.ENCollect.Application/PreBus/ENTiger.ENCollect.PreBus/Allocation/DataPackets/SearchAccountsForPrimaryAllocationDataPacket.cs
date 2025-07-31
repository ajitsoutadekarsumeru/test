using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountsForPrimaryAllocationDataPacket : FlexiFlowDataPacketWithDtoBridge<SearchAccountsForPrimaryAllocationDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchAccountsForPrimaryAllocationDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SearchAccountsForPrimaryAllocationDataPacket(ILogger<SearchAccountsForPrimaryAllocationDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}