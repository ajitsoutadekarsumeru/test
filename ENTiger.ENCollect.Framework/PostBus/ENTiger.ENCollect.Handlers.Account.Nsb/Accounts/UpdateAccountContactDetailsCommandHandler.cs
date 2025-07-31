using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateAccountContactDetailsCommandHandler : NsbCommandHandler<UpdateAccountContactDetailsCommand>
    {
        readonly ILogger<UpdateAccountContactDetailsCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateAccountContactDetailsCommandHandler(ILogger<UpdateAccountContactDetailsCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateAccountContactDetailsCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateAccountContactDetailsCommandHandler: {nameof(UpdateAccountContactDetailsCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateAccountContactDetailsPostBusDataPacket, UpdateAccountContactDetailsPostBusSequence, 
                UpdateAccountContactDetailsCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
