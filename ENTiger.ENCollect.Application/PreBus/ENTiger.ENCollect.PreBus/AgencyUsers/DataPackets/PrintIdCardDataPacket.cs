using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrintIdCardDataPacket : FlexiFlowDataPacketWithDtoBridge<PrintIdCardDto, FlexAppContextBridge>
    {
        protected readonly ILogger<PrintIdCardDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public PrintIdCardDataPacket(ILogger<PrintIdCardDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string IdCardNumber { get; set; }

        #endregion "Properties
    }
}