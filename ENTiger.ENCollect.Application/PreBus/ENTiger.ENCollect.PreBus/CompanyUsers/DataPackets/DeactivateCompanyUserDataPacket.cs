using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeactivateCompanyUserDataPacket : FlexiFlowDataPacketWithDtoBridge<DeactivateCompanyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DeactivateCompanyUserDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DeactivateCompanyUserDataPacket(ILogger<DeactivateCompanyUserDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}