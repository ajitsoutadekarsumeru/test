using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentRoundRobinFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentRoundRobinFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ITreatmentCommonFunctions _treatmentCommonFunctions;

        public ExecuteTreatmentRoundRobinFFPlugin(ILogger<ExecuteTreatmentRoundRobinFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, ITreatmentCommonFunctions treatmentCommonFunctions)
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
                    if (subtreatments?.RoundRobinCriteria.Count > 0)
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

                        //if (!string.IsNullOrEmpty(StartDay) && !string.IsNullOrEmpty(EndDay) && currDay >= Convert.ToInt32(StartDay) && currDay <= Convert.ToInt32(EndDay))
                        //if (subtreatments.RoundRobinCriteria.Count > 0)
                        if (DateTime.Now.Date >= subtreatmentstartdate.Value.Date && DateTime.Now.Date <= subtreatmentenddate.Value.Date)
                        //if (subtreatmentstartdate.Value.Date >= DateTime.Now.Date && DateTime.Now.Date <= subtreatmentenddate.Value.Date)
                        {
                            List<ElasticSearchSimulateLoanAccountDto> finalListOfLoanAccounts = new List<ElasticSearchSimulateLoanAccountDto>();
                            finalListOfLoanAccounts = await _treatmentCommonFunctions.FetchAccountsForAllocationAsync(packet.Cmd.Dto.loanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TenantId, packet.Cmd.Dto.TreatmentId, packet.Cmd.Dto.executeTreatmentDto);

                            string allocationType = subtreatments.AllocationType;

                            await RoundRobinAllocation(subtreatments, packet.Cmd.Dto.TreatmentId, packet, allocationType, finalListOfLoanAccounts, subtreatments.Id);
                        }
                    }
                }
            }
            _logger.LogInformation("Completed TreatmentRoundRobinFFPlugin ");

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task RoundRobinAllocation(SubTreatment subtreatments, string TreatmentId, ExecuteFragmentedTreatmentDataPacket packet, string allocationType, List<ElasticSearchSimulateLoanAccountDto> finalListOfLoanAccounts, string subtreatmentid)
        {
            List<string> accountids = new List<string>();
            //accountids = finalListOfLoanAccounts.Select(a => a.id).ToList();

            if (subtreatments.QualifyingCondition != null && string.Equals(subtreatments.QualifyingCondition, "yes", StringComparison.OrdinalIgnoreCase))
            {
                var sub = packet.outputModel.subTreatment.Where(a => a.Order == subtreatments.PreSubtreatmentOrder).FirstOrDefault();

                if (sub != null)
                {
                    subtreatmentid = sub.Id;
                }
                List<string> deliverystatuses = subtreatments.DeliveryStatus.Select(a => a.Status).ToList();
                var agreementids = await _treatmentCommonFunctions.FetchAccountsBasedOnMultipleQualifyingCondition(finalListOfLoanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TenantId, deliverystatuses, packet.Cmd.Dto.TreatmentId, subtreatmentid, packet.Cmd.Dto.TreatmentHistoryId, packet.Cmd.Dto.executeTreatmentDto);
                accountids = finalListOfLoanAccounts.Where(a => agreementids.Contains(a.agreementid)).Select(a => a.id).ToList();
            }
            else
            {
                accountids = finalListOfLoanAccounts.Select(a => a.id).ToList();
            }

            List<LoanAccount> accounts = new List<LoanAccount>();

            accounts = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(a => accountids.Contains(a.Id)).ToListAsync();

            if (accounts.Count > 0)
            {
                string[] allocationIds = subtreatments.RoundRobinCriteria.Select(a => a.AllocationId).Distinct().ToArray();
                List<LoanAccount> accountToSave = new List<LoanAccount>();

                int allocationidscount = allocationIds.Count();
                for (int i = 0; i < allocationIds.Count();)
                {
                    int countvalue = i;
                    LoanAccount acc = accounts.FirstOrDefault();
                    if (acc != null)
                    {
                        acc = _treatmentCommonFunctions.AssignToAccount(allocationIds[i].ToString(), acc, TreatmentId, allocationType);
                        //AssignToAccount(allocationIds[i].ToString(), acc, TreatmentId, allocationType);
                    }
                    else
                    {
                        break;
                    }

                    if (countvalue == allocationidscount - 1)
                    {
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }
                    accountToSave.Add(acc);
                    accounts.Remove(acc);
                }
                await _treatmentCommonFunctions.ConstructDatatable(packet, accountToSave, packet.Cmd.Dto.executeTreatmentDto);
            }
        }

        private void AssignToAccount(string allocationId, LoanAccount acc, string TreatmentId, string allocationType)
        {
            try
            {
                if (string.Equals(allocationType, AllocationTypeEnum.TeleCallingAgency.Value, StringComparison.OrdinalIgnoreCase))
                {
                    acc.TeleCallingAgencyId = allocationId.ToString();
                    acc.TreatmentId = TreatmentId;
                    acc.TeleCaller = null;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgency.Value, StringComparison.OrdinalIgnoreCase))
                {
                    acc.AgencyId = allocationId.ToString();
                    acc.TreatmentId = TreatmentId;
                    acc.CollectorId = null;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value, StringComparison.OrdinalIgnoreCase))
                {
                    acc.AllocationOwnerId = allocationId.ToString();
                    acc.TreatmentId = TreatmentId;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value, StringComparison.OrdinalIgnoreCase))
                {
                    acc.AllocationOwnerId = allocationId.ToString();
                    acc.TreatmentId = TreatmentId;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value, StringComparison.OrdinalIgnoreCase))
                {
                    acc.TeleCallerId = allocationId.ToString();
                    acc.TreatmentId = TreatmentId;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value, StringComparison.OrdinalIgnoreCase))
                {
                    acc.CollectorId = allocationId.ToString();
                    acc.TreatmentId = TreatmentId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in AssignToAccount " + ex);
            }
        }

    }
}