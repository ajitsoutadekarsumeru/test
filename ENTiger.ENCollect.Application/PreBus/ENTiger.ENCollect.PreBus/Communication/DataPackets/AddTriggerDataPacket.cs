using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddTriggerDataPacket : FlexiFlowDataPacketWithDtoBridge<AddTriggerDto, FlexAppContextBridge>
    {

        protected readonly ILogger<AddTriggerDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddTriggerDataPacket(ILogger<AddTriggerDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
