using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddUserAttendanceDataPacket : FlexiFlowDataPacketWithDtoBridge<AddUserAttendanceDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddUserAttendanceDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddUserAttendanceDataPacket(ILogger<AddUserAttendanceDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}