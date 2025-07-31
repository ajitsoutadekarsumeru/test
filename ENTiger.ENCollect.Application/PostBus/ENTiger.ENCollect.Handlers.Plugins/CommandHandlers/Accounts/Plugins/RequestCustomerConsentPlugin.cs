using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class RequestCustomerConsentPlugin : FlexiPluginBase, IFlexiPlugin<RequestCustomerConsentPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a18827c8f6d6da84da439a87cbb5b9d";
        public override string FriendlyName { get; set; } = "RequestCustomerConsentPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<RequestCustomerConsentPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ICustomUtility _customUtility;

        protected CustomerConsent? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly FrontendUrlSettings _urlSettings;

        public RequestCustomerConsentPlugin(ILogger<RequestCustomerConsentPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, ICustomUtility customUtility, IOptions<FrontendUrlSettings> urlSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
            _urlSettings = urlSettings.Value;
           
        }

        public virtual async Task Execute(RequestCustomerConsentPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<CustomerConsent>().RequestCustomerConsent(packet.Cmd);
            
            _repoFactory.GetRepo().InsertOrUpdate(_model);

            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                EventCondition = CONDITION_ONSUCCESS;
                _logger.LogInformation($"{typeof(CustomerConsent).Name} with {_model.Id} inserted into Database");
            }
            else
            {
                _logger.LogWarning($"No records inserted for {typeof(CustomerConsent).Name} with {_model.Id}");
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}