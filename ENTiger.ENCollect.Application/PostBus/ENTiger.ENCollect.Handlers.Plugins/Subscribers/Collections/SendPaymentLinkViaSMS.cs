using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentLinkViaSMS : ISendPaymentLinkViaSMS
    {
        protected readonly ILogger<SendPaymentLinkViaSMS> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly PaymentGatewayFactory _paymentGatewayFactory;

        public SendPaymentLinkViaSMS(ILogger<SendPaymentLinkViaSMS> logger, IRepoFactory repoFactory, PaymentGatewayFactory paymentGatewayFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _paymentGatewayFactory = paymentGatewayFactory;
        }

        public virtual async Task Execute(PaymentLinkGeneratedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);

            var collection = await repo.GetRepo().FindAll<Collection>()
                                   .ByCollectionId(@event.CollectionId)
                                   .SelectTo<CollectionDtoWithId>()
                                   .FirstOrDefaultAsync();

            collection.SetAppContext(@event.AppContext);

            var paymentTransaction = await repo.GetRepo().FindAll<PaymentTransaction>()
                                        .Where(p => p.Id == @event.PaymentTransactionId)
                                        .SelectTo<PaymentTransactionDtoWithId>()
                                        .FirstOrDefaultAsync();

            var paymentGateway = _paymentGatewayFactory.GetPaymentGateway(@event.PaymentPartner);

            await paymentGateway.SendPaymentLinkSMS(collection, paymentTransaction, @event.AppContext.TenantId);

            await this.Fire<SendPaymentLinkViaSMS>(EventCondition, serviceBusContext);
        }
    }
}