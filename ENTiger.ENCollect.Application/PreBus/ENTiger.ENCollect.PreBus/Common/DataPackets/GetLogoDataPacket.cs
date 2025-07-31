using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetLogoDataPacket : FlexiFlowDataPacketWithDtoBridge<GetLogoDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetLogoDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetLogoDataPacket(ILogger<GetLogoDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string Logo { get; set; }

        #endregion "Properties
    }
}