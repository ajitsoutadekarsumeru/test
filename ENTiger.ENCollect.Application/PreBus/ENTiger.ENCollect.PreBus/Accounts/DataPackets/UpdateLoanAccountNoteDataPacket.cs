using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateLoanAccountNoteDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateLoanAccountNoteDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateLoanAccountNoteDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountNoteDataPacket(ILogger<UpdateLoanAccountNoteDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}