using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateCodeOfConductCommandHandler : NsbCommandHandler<UpdateCodeOfConductCommand>
    {
        readonly ILogger<UpdateCodeOfConductCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCodeOfConductCommandHandler(ILogger<UpdateCodeOfConductCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateCodeOfConductCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateCodeOfConductCommandHandler: {nameof(UpdateCodeOfConductCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateCodeOfConductPostBusDataPacket, UpdateCodeOfConductPostBusSequence, 
                UpdateCodeOfConductCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
