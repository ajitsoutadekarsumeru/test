using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationByFilterDataPacket : FlexiFlowDataPacketWithDtoBridge<SecondaryAllocationByFilterDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SecondaryAllocationByFilterDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SecondaryAllocationByFilterDataPacket(ILogger<SecondaryAllocationByFilterDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}