using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountNotePlugin : FlexiPluginBase, IFlexiPlugin<AddLoanAccountNotePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a15a220ee6597d534672397ec75e747";
        public override string FriendlyName { get; set; } = "AddLoanAccountNotePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddLoanAccountNotePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected LoanAccountNote? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddLoanAccountNotePlugin(ILogger<AddLoanAccountNotePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddLoanAccountNotePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<LoanAccountNote>().AddLoanAccountNote(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(LoanAccountNote).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(LoanAccountNote).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}