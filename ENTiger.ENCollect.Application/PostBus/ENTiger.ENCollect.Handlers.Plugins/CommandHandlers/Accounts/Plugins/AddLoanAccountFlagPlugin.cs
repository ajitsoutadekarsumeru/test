using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountFlagPlugin : FlexiPluginBase, IFlexiPlugin<AddLoanAccountFlagPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a15a2224fa70153be6a398aaae97987";
        public override string FriendlyName { get; set; } = "AddLoanAccountFlagPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddLoanAccountFlagPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected LoanAccountFlag? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddLoanAccountFlagPlugin(ILogger<AddLoanAccountFlagPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddLoanAccountFlagPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<LoanAccountFlag>().AddLoanAccountFlag(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(LoanAccountFlag).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(LoanAccountFlag).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}