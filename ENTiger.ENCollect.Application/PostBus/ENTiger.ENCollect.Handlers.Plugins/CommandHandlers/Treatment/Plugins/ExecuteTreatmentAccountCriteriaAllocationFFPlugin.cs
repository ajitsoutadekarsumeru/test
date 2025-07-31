using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentAccountCriteriaAllocationFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentAccountCriteriaAllocationFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ITreatmentCommonFunctions _treatmentCommonFunctions;

        public ExecuteTreatmentAccountCriteriaAllocationFFPlugin(ILogger<ExecuteTreatmentAccountCriteriaAllocationFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, ITreatmentCommonFunctions treatmentCommonFunctions)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _treatmentCommonFunctions = treatmentCommonFunctions;
        }
        public virtual async Task Execute(ExecuteFragmentedTreatmentDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto.executeTreatmentDto);
            if (packet.Cmd.Dto.totalCountOfAccounts > 0)
            {
                foreach (var subtreatments in packet.outputModel.subTreatment)
                {
                    if (subtreatments?.AccountCriteria.Count > 0)
                    {
                        int currDay = DateTime.Now.Day;
                        string StartDay = subtreatments.StartDay;
                        string EndDay = subtreatments.EndDay;

                        DateTime? treatmentexecutionstartdate = packet.outputModel.ExecutionStartdate;
                        DateTime? treatmentexecutionenddate = packet.outputModel.ExecutionStartdate;

                        DateTime? subtreatmentstartdate = DateTime.Now;
                        DateTime? subtreatmentenddate = DateTime.Now;

                        if (string.IsNullOrEmpty(StartDay))
                        {
                            StartDay = "1";
                        }
                        else
                        {
                            _logger.LogInformation("treatmentexecutionstartdate " + treatmentexecutionstartdate);
                            int startdate = Convert.ToInt32(StartDay);

                            if (treatmentexecutionstartdate != null)
                            {
                                subtreatmentstartdate = treatmentexecutionstartdate.Value.AddDays(startdate - 1);
                            }
                            else
                            {
                                subtreatmentstartdate = DateTime.Now;
                            }
                        }
                        if (string.IsNullOrEmpty(EndDay))
                        {
                            EndDay = "31";
                        }
                        else
                        {
                            _logger.LogInformation("treatmentexecutionenddate " + treatmentexecutionstartdate);
                            int enddate = Convert.ToInt32(EndDay);

                            if (treatmentexecutionenddate != null)
                            {
                                subtreatmentenddate = treatmentexecutionenddate.Value.AddDays(enddate - 1);
                            }
                            else
                            {
                                subtreatmentenddate = DateTime.Now;
                            }
                        }

                        if (DateTime.Now.Date >= subtreatmentstartdate.Value.Date && DateTime.Now.Date <= subtreatmentenddate.Value.Date)
                        {
                            List<ElasticSearchSimulateLoanAccountDto> finalListOfLoanAccounts = new List<ElasticSearchSimulateLoanAccountDto>();
                            finalListOfLoanAccounts = await _treatmentCommonFunctions.FetchAccountsForAllocationAsync(packet.Cmd.Dto.loanAccounts,
                                subtreatments.AllocationType, packet.Cmd.Dto.TenantId, packet.Cmd.Dto.TreatmentId, packet.Cmd.Dto.executeTreatmentDto);

                            string allocationType = subtreatments.AllocationType;
                            AccountCriteriaAllocation(packet, packet.Cmd.Dto.totalCountOfAccounts, subtreatments, allocationType, finalListOfLoanAccounts, subtreatments.Id);
                        }
                    }
                }
            }
            _logger.LogInformation("Completed TreatmentAccountCriteriaAllocationFFPlugin ");
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
        private async Task AccountCriteriaAllocation(ExecuteFragmentedTreatmentDataPacket packet, int totalCountOfAccounts, SubTreatment subtreatments, string allocationType, List<ElasticSearchSimulateLoanAccountDto> finalListOfLoanAccounts, string subtreatmentid)
        {
            List<string> accountids = new List<string>();
            if (!string.IsNullOrEmpty(subtreatments.QualifyingCondition) && string.Equals(subtreatments.QualifyingCondition, "yes"))
            {
                var sub = packet.outputModel.subTreatment.Where(a => a.Order == subtreatments.PreSubtreatmentOrder).FirstOrDefault();
                if (sub != null)
                {
                    subtreatmentid = sub.Id;
                }
                List<string> deliverystatuses = subtreatments.DeliveryStatus.Select(a => a.Status).ToList();
                var agreementids = await _treatmentCommonFunctions.FetchAccountsBasedOnMultipleQualifyingCondition(finalListOfLoanAccounts
                        ,subtreatments.AllocationType, packet.Cmd.Dto.TenantId, deliverystatuses,packet.Cmd.Dto.TreatmentId
                        ,subtreatmentid, packet.Cmd.Dto.TreatmentHistoryId, packet.Cmd.Dto.executeTreatmentDto);
                accountids = finalListOfLoanAccounts.Where(a => agreementids.Contains(a.agreementid)).Select(a => a.id).ToList();
            }
            else
            {
                accountids = finalListOfLoanAccounts.Select(a => a.id).ToList();
            }

            List<LoanAccount> accounts = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(a => accountids.Contains(a.Id)).ToListAsync();

            if (accounts.Count > 0)
            {
                List<LoanAccount> accountToSave = new List<LoanAccount>();
                try
                {
                    List<TreatmentAllocationDetail> allocations = subtreatments.AccountCriteria.
                        Select(a => new TreatmentAllocationDetail { AllocationId = a.AllocationId, Percentage = a.Percentage }).Distinct().ToList();
                    List<TreatmentAllocationDetail> countlist = new List<TreatmentAllocationDetail>();
                    foreach (var count in allocations)
                    {
                        decimal? percentage = Convert.ToDecimal(count.Percentage);
                        TreatmentAllocationDetail alloc = new TreatmentAllocationDetail();
                        alloc.AllocationId = count.AllocationId;
                        alloc.Percentage = count.Percentage;
                        alloc.Count = Math.Round(Convert.ToDouble(((totalCountOfAccounts * percentage) / 100)));
                        countlist.Add(alloc);
                    }

                    foreach (var alloc in countlist)
                    {
                        int noOfAccount = Convert.ToInt32(alloc.Count);
                        List<LoanAccount> loanaccountlist = accounts.Take(noOfAccount).ToList();

                        foreach (var acc1 in loanaccountlist)
                        {
                            var tempaccount = acc1;
                            tempaccount = _treatmentCommonFunctions.AssignToAccount(alloc.AllocationId.ToString(), acc1, packet.Cmd.Dto.TreatmentId, allocationType); ;

                            //AssignToAccount(alloc.AllocationId.ToString(), acc1, packet.TreatmentId, allocationType);
                            accountToSave.Add(tempaccount);
                            accounts.Remove(tempaccount);
                        }
                    }
                    await _treatmentCommonFunctions.ConstructDatatable(packet, accountToSave, packet.Cmd.Dto.executeTreatmentDto);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception in AccountCriteriaAllocatoin " + ex);
                    throw ex;
                }
            }
        }
    }
}