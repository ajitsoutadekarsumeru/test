using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentPOSCriteriaAllocationFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentPOSCriteriaAllocationFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ITreatmentCommonFunctions _treatmentCommonFunctions;

        public ExecuteTreatmentPOSCriteriaAllocationFFPlugin(ILogger<ExecuteTreatmentPOSCriteriaAllocationFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, ITreatmentCommonFunctions treatmentCommonFunctions)
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

            _logger.LogInformation("Entered in TreatmentPOSCriteriaAllocationFFPlugin ");

            try
            {
                if (packet.Cmd.Dto.totalCountOfAccounts > 0)
                {
                    foreach (var subtreatments in packet.outputModel.subTreatment)
                    {
                        if (subtreatments?.POSCriteria.Count > 0)
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
                            //if (subtreatments.POSCriteria.Count > 0)

                            if (DateTime.Now.Date >= subtreatmentstartdate.Value.Date && DateTime.Now.Date <= subtreatmentenddate.Value.Date)
                            {
                                List<ElasticSearchSimulateLoanAccountDto> finalListOfLoanAccounts = new List<ElasticSearchSimulateLoanAccountDto>();
                                finalListOfLoanAccounts = await _treatmentCommonFunctions.FetchAccountsForAllocationAsync(packet.Cmd.Dto.loanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TenantId, packet.Cmd.Dto.TreatmentId, packet.Cmd.Dto.executeTreatmentDto);

                                List<string> accountids = new List<string>();

                                if (subtreatments.QualifyingCondition != null && string.Equals(subtreatments.QualifyingCondition, "yes", StringComparison.OrdinalIgnoreCase))
                                {
                                    string subtreatmenid = string.Empty;
                                    var sub = packet.outputModel.subTreatment.Where(a => a.Order == subtreatments.PreSubtreatmentOrder).FirstOrDefault();

                                    if (sub != null)
                                    {
                                        subtreatmenid = sub.Id;
                                    }

                                    List<string> deliverystatuses = subtreatments.DeliveryStatus.Select(a => a.Status).ToList();
                                    var agreementids = await _treatmentCommonFunctions.FetchAccountsBasedOnMultipleQualifyingCondition(finalListOfLoanAccounts
                                                                    ,subtreatments.AllocationType, packet.Cmd.Dto.TenantId, deliverystatuses
                                                                    ,packet.Cmd.Dto.TreatmentId, subtreatmenid, packet.Cmd.Dto.TreatmentHistoryId
                                                                    ,packet.Cmd.Dto.executeTreatmentDto);

                                    accountids = finalListOfLoanAccounts.Where(a => agreementids.Contains(a.agreementid)).Select(a => a.id).ToList();
                                }
                                else
                                {
                                    accountids = finalListOfLoanAccounts.Select(a => a.id).ToList();
                                }

                                List<LoanAccount> accounts = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(a => accountids.Contains(a.Id)).ToListAsync();

                                if (accounts.Count > 0)
                                {
                                    string allocationType = subtreatments.AllocationType;

                                    await POSCriteriaAllocation(packet, subtreatments, accounts, allocationType);
                                }
                            }
                        }
                    }
                }
                _logger.LogInformation("Completed TreatmentPOSCriteriaAllocationFFPlugin ");
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message + " > " + ex.StackTrace + " > " + ex.ToString();
                if (ex.InnerException != null)
                    errorMsg += " >> " + ex.InnerException.Message + " > " + ex.InnerException.StackTrace + " > " + ex.InnerException.ToString();

                _logger.LogInformation("Exception in TreatmentPOSCriteriaAllocation " + errorMsg);
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task POSCriteriaAllocation(ExecuteFragmentedTreatmentDataPacket packet, SubTreatment subTreatments, List<LoanAccount> laccounts, string allocationType)
        {
            try
            {
                List<LoanAccount> accounts = new List<LoanAccount>();
                accounts = laccounts;
                List<LoanAccount> accountToSave = new List<LoanAccount>();
                List<LoanAccount> posaccounts = new List<LoanAccount>();
                posaccounts = accounts;
                List<TreatmentAllocationDetail> posallocations = subTreatments.POSCriteria.
                Select(a => new TreatmentAllocationDetail { AllocationId = a.AllocationId, Percentage = a.Percentage }).Distinct().ToList();

                List<TreatmentAllocationDetail> poscountlist = new List<TreatmentAllocationDetail>();
                double totalPOSAmount = Convert.ToDouble(accounts.Sum(a => a.BOM_POS));
                foreach (var poscount in posallocations)
                {
                    decimal? percentage = Convert.ToDecimal(poscount.Percentage);
                    TreatmentAllocationDetail alloc = new TreatmentAllocationDetail();
                    alloc.AllocationId = poscount.AllocationId;
                    alloc.Percentage = poscount.Percentage;
                    alloc.Amount = Convert.ToDouble(((totalPOSAmount * Convert.ToDouble(percentage)) / 100));
                    // alloc.Amount = Math.Round(Convert.ToDouble(((totalPOSAmount * percentage) / 100)));
                    poscountlist.Add(alloc);
                }

                if (poscountlist.Count > 0)
                {
                    foreach (var posalloc in poscountlist)
                    {
                        double? posamount = posalloc.Amount;
                        //var loanaccountlist = accounts.Take(2).Sum(a=>a.BOM_POS).ToList();
                        List<LoanAccount> tempaccountlist = new List<LoanAccount>();
                        foreach (var posacc1 in accounts)
                        {
                            double? bompos = Convert.ToDouble(posacc1.BOM_POS);
                            double? tempbompos = Convert.ToDouble(tempaccountlist.Sum(a => a.BOM_POS));
                            if (bompos < posamount && (tempaccountlist.Count == 0))
                            {
                                tempaccountlist.Add(posacc1);
                            }
                            else if (tempbompos <= posamount)
                            {
                                if ((posamount - tempbompos) <= 100)
                                {
                                    tempaccountlist.Add(posacc1);
                                    foreach (var tempacc in tempaccountlist)
                                    {
                                        var tempaccount = tempacc;
                                        tempaccount = _treatmentCommonFunctions.AssignToAccount(posalloc.AllocationId.ToString(), tempacc, packet.Cmd.Dto.TreatmentId, allocationType);
                                        //AssignToAccount(posalloc.AllocationId.ToString(), tempacc, packet.TreatmentId, allocationType);
                                        accountToSave.Add(tempaccount);
                                        posaccounts.Remove(tempaccount);
                                    }
                                    tempaccountlist.Clear();
                                    break;
                                }
                                else if ((accounts.Count - 1) == tempaccountlist.Count)
                                {
                                    tempaccountlist.Add(posacc1);
                                    foreach (var tempacc in tempaccountlist)
                                    {
                                        var tempaccount = tempacc;
                                        tempaccount = _treatmentCommonFunctions.AssignToAccount(posalloc.AllocationId.ToString(), tempacc, packet.Cmd.Dto.TreatmentId, allocationType);
                                        //AssignToAccount(posalloc.AllocationId.ToString(), tempacc, packet.TreatmentId, allocationType);

                                        accountToSave.Add(tempaccount);
                                        posaccounts.Remove(tempaccount);
                                    }
                                    tempaccountlist.Clear();
                                    break;
                                }
                                else
                                {
                                    tempaccountlist.Add(posacc1);
                                }
                            }
                            else if (tempbompos >= posamount)
                            {
                                foreach (var tempacc in tempaccountlist)
                                {
                                    var tempaccount = tempacc;
                                    tempaccount = _treatmentCommonFunctions.AssignToAccount(posalloc.AllocationId.ToString(), tempacc, packet.Cmd.Dto.TreatmentId, allocationType);
                                    //AssignToAccount(posalloc.AllocationId.ToString(), tempacc, packet.TreatmentId, allocationType);

                                    accountToSave.Add(tempaccount);
                                    posaccounts.Remove(tempaccount);
                                }
                                tempaccountlist.Clear();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var posacc1 in accounts)
                    {
                        string allocationid = poscountlist.Select(a => a.AllocationId).FirstOrDefault();
                        var tempaccount = posacc1;
                        tempaccount = _treatmentCommonFunctions.AssignToAccount(allocationid, posacc1, packet.Cmd.Dto.TreatmentId, allocationType);
                        accountToSave.Add(posacc1);
                    }
                }
                await _treatmentCommonFunctions.ConstructDatatable(packet, accountToSave, packet.Cmd.Dto.executeTreatmentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in POSCriteriaAllocation " + ex);
                throw;
            }
        }
    }
}