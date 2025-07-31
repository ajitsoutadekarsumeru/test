using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipImageDataPacket : FlexiFlowDataPacketWithDtoBridge<GetPayInSlipImageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetPayInSlipImageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetPayInSlipImageDataPacket(ILogger<GetPayInSlipImageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}