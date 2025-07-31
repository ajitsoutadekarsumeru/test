using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCompanyUserDataPacket : FlexiFlowDataPacketWithDtoBridge<AddCompanyUserDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddCompanyUserDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddCompanyUserDataPacket(ILogger<AddCompanyUserDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}