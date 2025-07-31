using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddTemplateDataPacket : FlexiFlowDataPacketWithDtoBridge<AddTemplateDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddTemplateDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddTemplateDataPacket(ILogger<AddTemplateDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}