using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class CustomerConsentResponsePlugin : FlexiPluginBase, IFlexiPlugin<CustomerConsentResponsePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a18827d6fdbaa22c2fc887703082f2b";
        public override string FriendlyName { get; set; } = "CustomerConsentResponsePlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<CustomerConsentResponsePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CustomerConsent? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected string _userId;
        public CustomerConsentResponsePlugin(ILogger<CustomerConsentResponsePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CustomerConsentResponsePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var transactionId = packet.Cmd.Dto.ConsentId;

            _model = await _repoFactory.GetRepo().FindAll<CustomerConsent>().BySecureToken(transactionId).FirstOrDefaultAsync();
            
            if (_model != null)
            {
                _logger.LogInformation("CustomerConsent with Id {ModelId} update into Database.", _model.Id);
                _model.CustomerConsentResponse(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    EventCondition = CONDITION_ONSUCCESS;
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CustomerConsent).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CustomerConsent).Name, _model.Id);
                }
            }
            else
            {
                _logger.LogWarning($"{typeof(CustomerConsent).Name} with {packet.Cmd.Dto.ConsentId} not found in Database");
            }

 
            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}