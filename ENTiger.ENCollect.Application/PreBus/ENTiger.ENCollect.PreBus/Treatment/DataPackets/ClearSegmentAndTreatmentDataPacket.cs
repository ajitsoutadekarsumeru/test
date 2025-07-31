using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ClearSegmentAndTreatmentDataPacket : FlexiFlowDataPacketWithDtoBridge<ClearSegmentAndTreatmentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ClearSegmentAndTreatmentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ClearSegmentAndTreatmentDataPacket(ILogger<ClearSegmentAndTreatmentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}