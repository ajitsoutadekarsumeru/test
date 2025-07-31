using ENTiger.ENCollect.TreatmentModule;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentOnUpdateTrailFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentOnUpdateTrailFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ITreatmentCommonFunctions _treatmentCommonFunctions;

        public ExecuteTreatmentOnUpdateTrailFFPlugin(ILogger<ExecuteTreatmentOnUpdateTrailFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, ITreatmentCommonFunctions treatmentCommonFunctions)
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
                    if (subtreatments.TreatmentOnUpdateTrail != null)
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

                        if (subtreatmentstartdate.Value.Date >= DateTime.Now.Date && DateTime.Now.Date <= subtreatmentenddate.Value.Date)
                        {
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
                                var agreementids = await _treatmentCommonFunctions.FetchAccountsBasedOnMultipleQualifyingCondition(packet.Cmd.Dto.loanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TenantId, deliverystatuses, packet.Cmd.Dto.TreatmentId, subtreatmenid, packet.Cmd.Dto.TreatmentHistoryId, packet.Cmd.Dto.executeTreatmentDto);

                                accountids = packet.Cmd.Dto.loanAccounts.Where(a => agreementids.Contains(a.agreementid)).Select(a => a.id).ToList();
                            }
                            else
                            {
                                accountids = packet.Cmd.Dto.loanAccounts.Select(a => a.id).ToList();
                            }

                            if (accountids.Count > 0)
                            {
                                List<Feedback> feedbacklist = new List<Feedback>();

                                foreach (var accountId in accountids)
                                {
                                    Feedback feedback = new Feedback();
                                    feedback.SetAddedOrModified();
                                    feedback.SetId(SequentialGuid.NewGuidString());
                                    feedback.AccountId = accountId;
                                    feedback.SetCreatedBy(packet.Cmd.Dto.PartyId);
                                    feedback.SetLastModifiedBy(packet.Cmd.Dto.PartyId);
                                    feedback.DispositionGroup = subtreatments.TreatmentOnUpdateTrail.DispositionCodeGroup;
                                    feedback.DispositionCode = subtreatments.TreatmentOnUpdateTrail.DispositionCode;
                                    feedback.FeedbackDate = DateTime.Now;
                                    feedback.SetCreatedDate(DateTime.Now);
                                    feedback.SetLastModifiedDate(DateTime.Now);
                                    feedback.DispositionDate = DateTime.Now;
                                    feedback.CustomerMet = "No";

                                    if (subtreatments.TreatmentOnUpdateTrail.NextActionDate != null)
                                    {
                                        feedback.PTPDate = subtreatments.TreatmentOnUpdateTrail.NextActionDate;
                                    }
                                    if (string.Equals(subtreatments.TreatmentOnUpdateTrail.DispositionCode, "ptp", StringComparison.OrdinalIgnoreCase))
                                    {
                                        feedback.PTPAmount = subtreatments.TreatmentOnUpdateTrail.PTPAmount;
                                    }

                                    feedbacklist.Add(feedback);
                                }

                                foreach (var feedback in feedbacklist)
                                {
                                    _repoFactory.GetRepo().InsertOrUpdate(feedback);
                                }
                                int x =await _repoFactory.GetRepo().SaveAsync();
                            }
                        }
                    }
                }
            }
            _logger.LogInformation("Completed TreatmentOnUpdateTrailFFPlugin ");

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}