using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ExecuteTreatmentDataPacket : FlexiFlowDataPacketWithDtoBridge<ExecuteTreatmentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ExecuteTreatmentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ExecuteTreatmentDataPacket(ILogger<ExecuteTreatmentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}