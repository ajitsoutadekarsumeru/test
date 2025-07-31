using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateCustomerConsentExpiryPlugin : FlexiPluginBase, IFlexiPlugin<UpdateCustomerConsentExpiryPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a18ba622957970bee94eaf525f00cb3";
        public override string FriendlyName { get; set; } = "UpdateCustomerConsentExpiryPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<UpdateCustomerConsentExpiryPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CustomerConsent? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateCustomerConsentExpiryPlugin(ILogger<UpdateCustomerConsentExpiryPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateCustomerConsentExpiryPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            //await this call - add ToListAsync()
            var expiryList = await _repoFactory.GetRepo().FindAll<CustomerConsent>().ByDateToExpire(packet.Cmd.Dto.ExpiryDate.DateTime).ToListAsync(); 
            foreach (var item in expiryList)
            {
                _model = _flexHost.GetDomainModel<CustomerConsent>().UpdateCustomerConsentExpiry(item,_flexAppContext?.UserId);
                _repoFactory.GetRepo().InsertOrUpdate(_model);
            }
            
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
                {
                    _logger.LogInformation($"{records} records updated for {typeof(CustomerConsent).Name} with Expiry Date {packet.Cmd.Dto.ExpiryDate} in Database");
                }
                else
                {
                    _logger.LogWarning($"No records updated for {typeof(CustomerConsent).Name} with Expiry Date {packet.Cmd.Dto.ExpiryDate} ");
                }
        }
    }
}