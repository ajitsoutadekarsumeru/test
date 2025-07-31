using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DisableTreatmentsDataPacket : FlexiFlowDataPacketWithDtoBridge<DisableTreatmentsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DisableTreatmentsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DisableTreatmentsDataPacket(ILogger<DisableTreatmentsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}