using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdatePayuResponseDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdatePayuResponseDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdatePayuResponseDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePayuResponseDataPacket(ILogger<UpdatePayuResponseDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}