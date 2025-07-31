using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class GeneratePaymentLinkForOnlineCollection : IGeneratePaymentLinkForOnlineCollection
    {
        protected readonly ILogger<GeneratePaymentLinkForOnlineCollection> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly PaymentGatewayFactory _paymentGatewayFactory;
        protected readonly IFlexHost _flexHost;
        protected PaymentTransaction paymentTransaction;
        protected CollectionDtoWithId? collection;
        protected string paymentPartner;
        protected readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;

        public GeneratePaymentLinkForOnlineCollection(ILogger<GeneratePaymentLinkForOnlineCollection> logger,
            IRepoFactory repoFactory, PaymentGatewayFactory paymentGatewayFactory, IFlexHost flexHost)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _paymentGatewayFactory = paymentGatewayFactory;
            _flexHost = flexHost;
            _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
        }

        public virtual async Task Execute(OnlineCollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            paymentPartner = !string.IsNullOrEmpty(@event.PaymentPartner) ? @event.PaymentPartner : "razorpay";
            _logger.LogInformation("GeneratePaymentLinkForOnlineCollection : Online Payment - Start | PaymentPartner - " + paymentPartner);
            _flexAppContext = @event.AppContext; //do not remove this line
            var repo = _repoFactory.Init(@event);
            var tenantId = _flexAppContext.TenantId;
            collection = await repo.GetRepo().FindAll<Collection>().ByCollectionId(@event.CollectionId).SelectTo<CollectionDtoWithId>().FirstOrDefaultAsync();

            if (collection != null && !string.IsNullOrEmpty(collection.CollectionMode) && collection.CollectionMode.ToLower().StartsWith("online"))
            {
                List<FeatureMasterDtoWithId> paymentDetails = new List<FeatureMasterDtoWithId>();
                paymentDetails = await repo.GetRepo().FindAll<FeatureMaster>()
                                          .Where(a => a.Parameter.StartsWith(paymentPartner))
                                          .SelectTo<FeatureMasterDtoWithId>()
                                          .ToListAsync();


                //Get the Payment Gateway
                string? hostName = await _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == tenantId).Select(x => x.HostName).FirstOrDefaultAsync();
                //fetch the factory instance
                var utility = _flexHost.GetUtilityService<PaymentGatewayFactory>(hostName);

                //fetch the correct enum w.r.t the paymentPartner
                var paymentGatewayEnum = PaymentGatewayEnum.FromValue<PaymentGatewayEnum>(paymentPartner);
                var paymentGateway = utility.GetPaymentGateway(paymentGatewayEnum);

                //Call GetPaymentLinkDetails
                var paymentTransactionDto = await paymentGateway.GetPaymentLinkDetails(collection, paymentDetails, @event.AppContext.TenantId);
                paymentTransactionDto?.SetAppContext(@event.AppContext);

                //Save PaymentGateway Link details
                if (paymentTransactionDto != null)
                {
                    var tinyUrlFactory = _flexHost.GetUtilityService<TinyUrlFactory>(hostName);

                    if (tinyUrlFactory != null)
                    {
                        try
                        {
                            var urlProviderEnum = TinyUrlProviderTypeEnum.FromValue<TinyUrlProviderTypeEnum>(hostName);
                            var tinyUrlProvider = tinyUrlFactory.GetTinyUrlProvider(urlProviderEnum);

                            var returnUrl = paymentTransactionDto.PaymentGateway?.ReturnURL;
                            if (!string.IsNullOrWhiteSpace(returnUrl) && tinyUrlProvider != null)
                            {
                                paymentTransactionDto.PaymentGateway.ReturnURL =
                                    await tinyUrlProvider.CreateTinyUrlAsync(returnUrl, tenantId);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed to resolve TinyUrlProvider for host: {hostName}", hostName);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("TinyUrlFactory was null for host: {hostName}", hostName);
                    }

                    paymentTransaction = _flexHost.GetDomainModel<PaymentTransaction>().AddPaymentTransaction(paymentTransactionDto);
                    await SaveData();
                    EventCondition = CONDITION_ONSUCCESS;
                }
            }

            _logger.LogInformation("GeneratePaymentLinkForOnlineCollection : Online Payment - End");
            await this.Fire<GeneratePaymentLinkForOnlineCollection>(EventCondition, serviceBusContext);
        }

        private async Task SaveData()
        {
            _repoFactory.GetRepo().InsertOrUpdate(paymentTransaction);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(PaymentTransaction).Name, paymentTransaction.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(PaymentTransaction).Name, paymentTransaction.Id);
            }
        }
    }
}