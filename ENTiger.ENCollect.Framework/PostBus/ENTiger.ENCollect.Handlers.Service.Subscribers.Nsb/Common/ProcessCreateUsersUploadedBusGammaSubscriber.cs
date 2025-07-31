using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessCreateUsersUploadedBusGammaSubscriber : NsbSubscriberBridge<CreateUsersUploadedEvent>
    {
        readonly ILogger<ProcessCreateUsersUploadedBusGammaSubscriber> _logger;
        readonly IProcessCreateUsersUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessCreateUsersUploadedBusGammaSubscriber(ILogger<ProcessCreateUsersUploadedBusGammaSubscriber> logger, IProcessCreateUsersUploaded subscriber)
        {
            _logger = logger;
            _subscriber = subscriber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(CreateUsersUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
