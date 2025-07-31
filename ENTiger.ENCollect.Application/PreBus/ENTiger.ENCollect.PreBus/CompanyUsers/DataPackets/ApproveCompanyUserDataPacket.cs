using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveCompanyUserDataPacket : FlexiFlowDataPacketWithDtoBridge<ApproveCompanyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ApproveCompanyUserDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ApproveCompanyUserDataPacket(ILogger<ApproveCompanyUserDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}