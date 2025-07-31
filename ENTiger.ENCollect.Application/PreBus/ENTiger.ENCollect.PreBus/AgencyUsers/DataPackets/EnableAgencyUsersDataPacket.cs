using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EnableAgencyUsersDataPacket : FlexiFlowDataPacketWithDtoBridge<EnableAgencyUsersDto, FlexAppContextBridge>
    {

        protected readonly ILogger<EnableAgencyUsersDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EnableAgencyUsersDataPacket(ILogger<EnableAgencyUsersDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
