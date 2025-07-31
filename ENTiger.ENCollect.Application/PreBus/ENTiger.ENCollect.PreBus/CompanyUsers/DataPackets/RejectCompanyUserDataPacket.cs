using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectCompanyUserDataPacket : FlexiFlowDataPacketWithDtoBridge<RejectCompanyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<RejectCompanyUserDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public RejectCompanyUserDataPacket(ILogger<RejectCompanyUserDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}