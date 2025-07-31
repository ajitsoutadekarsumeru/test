using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryAllocationByFilterDataPacket : FlexiFlowDataPacketWithDtoBridge<PrimaryAllocationByFilterDto, FlexAppContextBridge>
    {
        protected readonly ILogger<PrimaryAllocationByFilterDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public PrimaryAllocationByFilterDataPacket(ILogger<PrimaryAllocationByFilterDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}