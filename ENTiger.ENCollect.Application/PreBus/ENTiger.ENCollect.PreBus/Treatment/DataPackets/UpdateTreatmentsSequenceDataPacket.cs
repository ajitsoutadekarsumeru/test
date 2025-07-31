using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateTreatmentsSequenceDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateTreatmentsSequenceDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateTreatmentsSequenceDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTreatmentsSequenceDataPacket(ILogger<UpdateTreatmentsSequenceDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}