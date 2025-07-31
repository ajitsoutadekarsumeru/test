using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserAttendanceCommandHandler : NsbCommandHandler<UpdateUserAttendanceCommand>
    {
        readonly ILogger<UpdateUserAttendanceCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUserAttendanceCommandHandler(ILogger<UpdateUserAttendanceCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateUserAttendanceCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateUserAttendanceCommandHandler: {nameof(UpdateUserAttendanceCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateUserAttendancePostBusDataPacket, UpdateUserAttendancePostBusSequence, 
                UpdateUserAttendanceCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
