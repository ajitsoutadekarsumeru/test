using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeleteTreatmentsDataPacket : FlexiFlowDataPacketWithDtoBridge<DeleteTreatmentsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DeleteTreatmentsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DeleteTreatmentsDataPacket(ILogger<DeleteTreatmentsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}