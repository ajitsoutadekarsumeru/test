using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class DormantAgencyUserDataPacket : FlexiFlowDataPacketWithDtoBridge<DormantAgencyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DormantAgencyUserDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DormantAgencyUserDataPacket(ILogger<DormantAgencyUserDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}