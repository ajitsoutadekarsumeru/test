using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateTemplateStatusDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateTemplateStatusDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateTemplateStatusDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTemplateStatusDataPacket(ILogger<UpdateTemplateStatusDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}