using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTreatmentReportFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetTreatmentReportFileDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetTreatmentReportFileDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetTreatmentReportFileDataPacket(ILogger<GetTreatmentReportFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}