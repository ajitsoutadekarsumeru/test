using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SecondaryAllocationByFilterPlugin : FlexiPluginBase, IFlexiPlugin<SecondaryAllocationByFilterPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139de5dc2982df55280585045be95d";
        public override string FriendlyName { get; set; } = "SecondaryAllocationByFilterPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SecondaryAllocationByFilterPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected LoanAccount? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private string? loggedInUserId = string.Empty;
        public ICollection<LoanAccount> loanAccount = new List<LoanAccount>();

        public SecondaryAllocationByFilterPlugin(ILogger<SecondaryAllocationByFilterPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(SecondaryAllocationByFilterPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            loggedInUserId = _flexAppContext.UserId;

            var inputmodel = packet.Cmd.Dto;
            loanAccount = await fetchLoanAccountAsync(loanAccount, inputmodel.AccountIds);

            if (inputmodel.AllocateByTos != null && inputmodel.AllocateByTos == true)
            {
                //To be implemented
            }
            if (inputmodel.AllocateByCount != null && inputmodel.AllocateByCount == true)
            {
                //To be implemented
            }
            if (!String.IsNullOrEmpty(inputmodel.TelecallerAgentId))
            {
                loanAccount.ToList().ForEach(t =>
                {
                    t.TeleCallerId = inputmodel.TelecallerAgentId;
                    t.SecondaryAllocationByFilter(t, loggedInUserId);
                    _repoFactory.GetRepo().InsertOrUpdate(t);
                }
                );
                int records = await _repoFactory.GetRepo().SaveAsync();
            }
            if (inputmodel.allocationExpireDate1 != null)
            {
                loanAccount.ToList().ForEach(t =>
                {
                    t.TeleCallerAllocationExpiryDate = inputmodel.allocationExpireDate1;
                    t.SecondaryAllocationByFilter(t, loggedInUserId);
                    _repoFactory.GetRepo().InsertOrUpdate(t);
                }
                );
                int records = await _repoFactory.GetRepo().SaveAsync();
            }
            if (!String.IsNullOrEmpty(inputmodel.AgentId))
            {
                loanAccount.ToList().ForEach(t =>
                {
                    t = FetchUserOrgId(inputmodel.AgentId, t);
                    t.SecondaryAllocationByFilter(t, loggedInUserId);
                    _repoFactory.GetRepo().InsertOrUpdate(t);
                });
                int records = await _repoFactory.GetRepo().SaveAsync();
            }
            if (inputmodel.allocationExpireDate2 != null)
            {
                loanAccount.ToList().ForEach(t =>
                {
                    t.AgentAllocationExpiryDate = ((inputmodel.AllocateToAgents != null && inputmodel.AllocateToAgents == true) ?
                    inputmodel.allocationExpireDate2 : inputmodel.allocationExpireDate1);
                    t.SecondaryAllocationByFilter(t, loggedInUserId);
                    _repoFactory.GetRepo().InsertOrUpdate(t);
                });

                int records = await _repoFactory.GetRepo().SaveAsync();
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<ICollection<LoanAccount>> fetchLoanAccountAsync(ICollection<LoanAccount> loanAccount, List<string> AccountIds)
        {
            try
            {
                loanAccount = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(x => AccountIds.Contains(x.Id)).ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.InnerException);
            }
            System.Diagnostics.Trace.WriteLine("Account Fetched" + loanAccount.Count());
            return loanAccount;
        }
        private LoanAccount FetchUserOrgId(string userId, LoanAccount account)
        {
            string baseBranchId = string.Empty;

            ApplicationUser user = _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == userId).FirstOrDefault();

            if (user == null)
            {
                _logger.LogWarning("User not found for userId: {UserId}", userId);
                return account;
            }

            if (user is CompanyUser companyUser)
            {
                account.CollectorId = userId;
                account.AgencyId = (user as CompanyUser)?.BaseBranchId;
            }
            else if (user is AgencyUser agencyUser)
            {
                if (agencyUser.AgencyId == account.AgencyId)
                {
                    account.CollectorId = userId;
                }
                else
                {
                    _logger.LogWarning("AgencyUser {UserId} does not match with account AgencyId {AgencyId}.", userId, account.AgencyId);
                }
            }

            return account;
        }
    }
}