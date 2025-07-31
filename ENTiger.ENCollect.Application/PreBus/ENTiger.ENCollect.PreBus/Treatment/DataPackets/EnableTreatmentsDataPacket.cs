using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class EnableTreatmentsDataPacket : FlexiFlowDataPacketWithDtoBridge<EnableTreatmentsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<EnableTreatmentsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public EnableTreatmentsDataPacket(ILogger<EnableTreatmentsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}