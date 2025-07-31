using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateLoanAccountNotePlugin : FlexiPluginBase, IFlexiPlugin<UpdateLoanAccountNotePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a15a2219c633ed8749b79b0ae6bd22e";
        public override string FriendlyName { get; set; } = "UpdateLoanAccountNotePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateLoanAccountNotePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected LoanAccountNote? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateLoanAccountNotePlugin(ILogger<UpdateLoanAccountNotePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateLoanAccountNotePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<LoanAccountNote>().Where(m => m.Id == packet.Cmd.Dto.Id).FirstOrDefaultAsync();

            if (_model != null)
            {
                _model.UpdateLoanAccountNote(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(LoanAccountNote).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(LoanAccountNote).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(LoanAccountNote).Name, packet.Cmd.Dto.Id);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}