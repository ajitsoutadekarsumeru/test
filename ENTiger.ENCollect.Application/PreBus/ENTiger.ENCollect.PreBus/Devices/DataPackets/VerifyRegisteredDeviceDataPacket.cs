using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyRegisteredDeviceDataPacket : FlexiFlowDataPacketWithDtoBridge<VerifyRegisteredDeviceDto, FlexAppContextBridge>
    {
        protected readonly ILogger<VerifyRegisteredDeviceDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public VerifyRegisteredDeviceDataPacket(ILogger<VerifyRegisteredDeviceDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string Key { get; set; }
        #endregion "Properties
    }
}