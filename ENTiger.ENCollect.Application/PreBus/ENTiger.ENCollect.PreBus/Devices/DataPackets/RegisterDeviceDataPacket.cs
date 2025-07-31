using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RegisterDeviceDataPacket : FlexiFlowDataPacketWithDtoBridge<RegisterDeviceDto, FlexAppContextBridge>
    {
        protected readonly ILogger<RegisterDeviceDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public RegisterDeviceDataPacket(ILogger<RegisterDeviceDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string message { get; set; }
        public string Key { get; set; }
        #endregion "Properties
    }
}