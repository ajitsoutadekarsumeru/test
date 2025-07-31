using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeleteTemplateDataPacket : FlexiFlowDataPacketWithDtoBridge<DeleteTemplateDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DeleteTemplateDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DeleteTemplateDataPacket(ILogger<DeleteTemplateDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}