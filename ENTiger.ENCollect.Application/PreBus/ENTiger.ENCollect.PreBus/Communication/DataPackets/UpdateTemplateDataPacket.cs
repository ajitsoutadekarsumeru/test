using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateTemplateDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateTemplateDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateTemplateDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTemplateDataPacket(ILogger<UpdateTemplateDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}