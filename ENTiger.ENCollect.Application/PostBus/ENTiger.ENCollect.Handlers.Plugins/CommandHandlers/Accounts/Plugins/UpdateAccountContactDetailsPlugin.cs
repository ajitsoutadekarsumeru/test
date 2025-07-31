using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateAccountContactDetailsPlugin : FlexiPluginBase, IFlexiPlugin<UpdateAccountContactDetailsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138eed3c662b8f429ea3f622b83b2d";
        public override string FriendlyName { get; set; } = "UpdateAccountContactDetailsPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateAccountContactDetailsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected LoanAccount? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateAccountContactDetailsPlugin(ILogger<UpdateAccountContactDetailsPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateAccountContactDetailsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(m => m.Id == packet.Cmd.Dto.AccountId).FirstOrDefaultAsync();

            if (_model != null)
            {
                _model.UpdateAccountContactDetails(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(LoanAccount).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(LoanAccount).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(LoanAccount).Name, packet.Cmd.Dto.AccountId);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}