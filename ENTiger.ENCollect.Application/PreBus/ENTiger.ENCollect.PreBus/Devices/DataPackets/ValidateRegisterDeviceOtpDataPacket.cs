using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DevicesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateRegisterDeviceOtpDataPacket : FlexiFlowDataPacketWithDtoBridge<ValidateRegisterDeviceOtpDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ValidateRegisterDeviceOtpDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ValidateRegisterDeviceOtpDataPacket(ILogger<ValidateRegisterDeviceOtpDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}