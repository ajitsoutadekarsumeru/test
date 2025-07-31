using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCollectionDataPacket : FlexiFlowDataPacketWithDtoBridge<AddCollectionDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddCollectionDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddCollectionDataPacket(ILogger<AddCollectionDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string Receiptid { get; set; }
        public string? ReservationId { get; set; }
        public string Message { get; set; }

        #endregion "Properties
    }
}