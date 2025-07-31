using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class PrimaryAllocationByFilterPlugin : FlexiPluginBase, IFlexiPlugin<PrimaryAllocationByFilterPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139de4c546236747a14cd57234df43";
        public override string FriendlyName { get; set; } = "PrimaryAllocationByFilterPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<PrimaryAllocationByFilterPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected LoanAccount? _model;
        protected FlexAppContextBridge? _flexAppContext;
        public ICollection<LoanAccount> loanAccount = new List<LoanAccount>();
        private string? loggedInUserId = string.Empty;

        public PrimaryAllocationByFilterPlugin(ILogger<PrimaryAllocationByFilterPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PrimaryAllocationByFilterPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            loggedInUserId = _flexAppContext.UserId;

            List<AgencyAllocationByFilterEventModel> modellist = new List<AgencyAllocationByFilterEventModel>();

            var inputmodel = packet.Cmd.Dto;

            if (inputmodel.AllocateByTos == true)
            {
            }
            else if (inputmodel.AllocateToAgency == true)
            {
                loanAccount = await fetchLoanAccountAsync(loanAccount, inputmodel.AccountIds);

                if (!String.IsNullOrEmpty(inputmodel.AgencyId))
                {
                    loanAccount.ToList().ForEach(t =>
                    {
                        t.AgencyId = inputmodel.AgencyId;
                        t.CollectorId = null;
                        t.AgentAllocationExpiryDate=null;
                        t.PrimaryAllocationByFilter(t, loggedInUserId);
                        _repoFactory.GetRepo().InsertOrUpdate(t);
                    }
                    );
                    int records = await _repoFactory.GetRepo().SaveAsync();
                }
                if (!String.IsNullOrEmpty(inputmodel.TelecallingAgencyId))
                {
                    loanAccount.ToList().ForEach(t =>
                    {
                        t.TeleCallingAgencyId = inputmodel.TelecallingAgencyId;  
                        t.TeleCallerId = null;
                        t.TeleCallerAllocationExpiryDate = null;
                        t.PrimaryAllocationByFilter(t, loggedInUserId);
                        _repoFactory.GetRepo().InsertOrUpdate(t);
                    }
                    );
                    int records = await _repoFactory.GetRepo().SaveAsync();
                }
            }
            else if (inputmodel.AllocateByCount == true)
            {
                loanAccount = await fetchLoanAccountAsync(loanAccount, inputmodel.AccountIds);
                if (!String.IsNullOrEmpty(inputmodel.AgencyId))
                {
                    loanAccount.ToList().ForEach(t =>
                    {
                        AgencyAllocationByFilterEventModel model = new AgencyAllocationByFilterEventModel();
                        t.AgencyId = inputmodel.AgencyId;
                        t.CollectorId = null;
                        t.AgentAllocationExpiryDate = null;
                        t.PrimaryAllocationByFilter(t, loggedInUserId);
                        _repoFactory.GetRepo().InsertOrUpdate(t);
                    });

                    int records = await _repoFactory.GetRepo().SaveAsync();
                }
                if (!String.IsNullOrEmpty(inputmodel.TelecallingAgencyId))
                {
                    loanAccount.ToList().ForEach(t =>
                    {
                        t.TeleCallingAgencyId = inputmodel.TelecallingAgencyId;
                        t.TeleCallerId = null;
                        t.TeleCallerAllocationExpiryDate = null;
                        t.PrimaryAllocationByFilter(t, loggedInUserId);
                        _repoFactory.GetRepo().InsertOrUpdate(t);
                    }

                    );
                    int records = await _repoFactory.GetRepo().SaveAsync();
                }
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<ICollection<LoanAccount>> fetchLoanAccountAsync(ICollection<LoanAccount> loanAccount, List<string> AccountIds)
        {
            loanAccount = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(x => AccountIds.Contains(x.Id)).ToListAsync();

            return loanAccount;
        }
    }
}