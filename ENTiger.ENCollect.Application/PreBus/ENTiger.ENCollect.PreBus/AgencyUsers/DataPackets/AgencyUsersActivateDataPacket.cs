using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersActivateDataPacket : FlexiFlowDataPacketWithDtoBridge<AgencyUsersActivateDto, FlexAppContextBridge>
    {

        protected readonly ILogger<AgencyUsersActivateDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AgencyUsersActivateDataPacket(ILogger<AgencyUsersActivateDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
