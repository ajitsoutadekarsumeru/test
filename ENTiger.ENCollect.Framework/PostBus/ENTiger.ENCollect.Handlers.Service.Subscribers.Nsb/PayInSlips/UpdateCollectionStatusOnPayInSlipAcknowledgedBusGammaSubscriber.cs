using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateCollectionStatusOnPayInSlipAcknowledgedBusGammaSubscriber : NsbSubscriberBridge<UpdateCollectionStatusOnPayInSlipAcknowledgedEvent>
    {
        readonly ILogger<UpdateCollectionStatusOnPayInSlipAcknowledgedBusGammaSubscriber> _logger;
        readonly IUpdateCollectionStatusForAckPayinslips _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCollectionStatusOnPayInSlipAcknowledgedBusGammaSubscriber(ILogger<UpdateCollectionStatusOnPayInSlipAcknowledgedBusGammaSubscriber> logger, IUpdateCollectionStatusForAckPayinslips subscriber)
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
        public override async Task Handle(UpdateCollectionStatusOnPayInSlipAcknowledgedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
