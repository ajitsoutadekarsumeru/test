using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateLoanAccountNoteCommandHandler : NsbCommandHandler<UpdateLoanAccountNoteCommand>
    {
        readonly ILogger<UpdateLoanAccountNoteCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateLoanAccountNoteCommandHandler(ILogger<UpdateLoanAccountNoteCommandHandler> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(UpdateLoanAccountNoteCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateLoanAccountNoteCommandHandler: {nameof(UpdateLoanAccountNoteCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateLoanAccountNotePostBusDataPacket, UpdateLoanAccountNotePostBusSequence, 
                UpdateLoanAccountNoteCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
