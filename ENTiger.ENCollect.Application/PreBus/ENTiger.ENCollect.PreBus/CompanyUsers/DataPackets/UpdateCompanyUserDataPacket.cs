using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateCompanyUserDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateCompanyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateCompanyUserDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCompanyUserDataPacket(ILogger<UpdateCompanyUserDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}