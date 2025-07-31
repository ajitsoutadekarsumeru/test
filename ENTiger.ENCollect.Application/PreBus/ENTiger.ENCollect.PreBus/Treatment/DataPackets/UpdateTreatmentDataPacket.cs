using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateTreatmentDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateTreatmentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateTreatmentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTreatmentDataPacket(ILogger<UpdateTreatmentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}