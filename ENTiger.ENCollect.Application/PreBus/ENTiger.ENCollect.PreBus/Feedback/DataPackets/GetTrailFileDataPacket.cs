using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTrailFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetTrailFileDto, FlexAppContextBridge>
    {

        protected readonly ILogger<GetTrailFileDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetTrailFileDataPacket(ILogger<GetTrailFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string FilePath { get; set; }

        #endregion
    }
}
