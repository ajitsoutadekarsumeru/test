using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class MakeDormantCompanyUserDataPacket : FlexiFlowDataPacketWithDtoBridge<DormantCompanyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MakeDormantCompanyUserDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public MakeDormantCompanyUserDataPacket(ILogger<MakeDormantCompanyUserDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}