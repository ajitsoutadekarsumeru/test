using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddTreatmentDataPacket : FlexiFlowDataPacketWithDtoBridge<AddTreatmentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddTreatmentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddTreatmentDataPacket(ILogger<AddTreatmentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}