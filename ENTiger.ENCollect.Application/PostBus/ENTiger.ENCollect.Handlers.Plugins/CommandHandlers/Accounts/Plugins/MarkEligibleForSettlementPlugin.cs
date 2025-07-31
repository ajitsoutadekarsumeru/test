using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class MarkEligibleForSettlementPlugin : FlexiPluginBase, IFlexiPlugin<MarkEligibleForSettlementPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a195057058b96a48bf16f5ee138d733";
        public override string FriendlyName { get; set; } = "MarkEligibleForSettlementPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<MarkEligibleForSettlementPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<LoanAccount>? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public MarkEligibleForSettlementPlugin(ILogger<MarkEligibleForSettlementPlugin> logger, 
            IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(MarkEligibleForSettlementPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                .Where(m => packet.Cmd.Dto.LoanAccountIds.Contains(m.Id))
                                .ToListAsync();

            if (_model != null)
            {
                foreach (var obj in _model)
                {
                    obj.MarkEligibleForSettlement(packet.Cmd);
                    _repoFactory.GetRepo().InsertOrUpdate(obj);
                }

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(LoanAccount).Name, packet.Cmd.Dto.LoanAccountIds.ToString());
                }
                else
                {
                    _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(LoanAccount).Name, packet.Cmd.Dto.LoanAccountIds.ToString());
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(LoanAccount).Name, packet.Cmd.Dto.LoanAccountIds.ToString());
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}