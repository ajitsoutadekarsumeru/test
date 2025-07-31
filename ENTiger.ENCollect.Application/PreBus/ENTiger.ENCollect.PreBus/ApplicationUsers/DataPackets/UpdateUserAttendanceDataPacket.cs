using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateUserAttendanceDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateUserAttendanceDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateUserAttendanceDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUserAttendanceDataPacket(ILogger<UpdateUserAttendanceDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}