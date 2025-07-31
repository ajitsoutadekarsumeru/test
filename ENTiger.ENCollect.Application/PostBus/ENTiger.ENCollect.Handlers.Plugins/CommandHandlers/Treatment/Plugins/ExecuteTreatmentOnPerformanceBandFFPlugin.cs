using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentOnPerformanceBandFFPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentOnPerformanceBandFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ITreatmentCommonFunctions _treatmentCommonFunctions;

        public ExecuteTreatmentOnPerformanceBandFFPlugin(ILogger<ExecuteTreatmentOnPerformanceBandFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, ITreatmentCommonFunctions treatmentCommonFunctions)
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
                string _TenantId = packet.Cmd.Dto.TenantId;

                foreach (var subtreatments in packet.outputModel.subTreatment)
                {
                    if (subtreatments.PerformanceBand != null && subtreatments.PerformanceBand.Count() > 0)
                    {
                        int currDay = DateTime.Now.Day;
                        string StartDay = subtreatments.StartDay;
                        string EndDay = subtreatments.EndDay;

                        DateTime? treatmentexecutionstartdate = packet.outputModel.ExecutionStartdate;
                        DateTime? treatmentexecutionenddate = packet.outputModel.ExecutionStartdate;

                        DateTime? subtreatmentstartdate = DateTime.Now;
                        DateTime? subtreatmentenddate = DateTime.Now;

                        var pbSubtreatmentlist = subtreatments.PerformanceBand.ToList();

                        string allocationType = subtreatments.AllocationType;

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
                            finalListOfLoanAccounts = await _treatmentCommonFunctions.FetchAccountsForAllocationAsync(packet.Cmd.Dto.loanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TenantId, packet.Cmd.Dto.TreatmentId, packet.Cmd.Dto.executeTreatmentDto);

                            if (subtreatments.QualifyingCondition != null && string.Equals(subtreatments.QualifyingCondition, "yes"))
                            {
                                string subtreatmenid = string.Empty;
                                var sub = packet.outputModel.subTreatment.Where(a => a.Order == subtreatments.PreSubtreatmentOrder).FirstOrDefault();

                                if (sub != null)
                                {
                                    subtreatmenid = sub.Id;
                                }
                                List<string> deliverystatuses = subtreatments.DeliveryStatus.Select(a => a.Status).ToList();
                                var agreementids = await _treatmentCommonFunctions.FetchAccountsBasedOnMultipleQualifyingCondition(finalListOfLoanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TenantId, deliverystatuses, packet.Cmd.Dto.TreatmentId, subtreatmenid, packet.Cmd.Dto.TreatmentHistoryId, packet.Cmd.Dto.executeTreatmentDto);

                                finalListOfLoanAccounts = finalListOfLoanAccounts.Where(a => agreementids.Contains(a.agreementid)).ToList();
                            }
                            else
                            {
                                finalListOfLoanAccounts = finalListOfLoanAccounts.ToList();
                            }

                            List<string> designationlist = subtreatments.Designation.Select(a => a.DesignationId).ToList();
                            List<string> performancebandlist = subtreatments.PerformanceBand.Select(a => a.PerformanceBand).ToList();

                            List<string> responsibleids = new List<string>();
                            List<string> customerpersonaids = new List<string>();
                            List<UserPersonaMaster> personamaster = new List<UserPersonaMaster>();

                            List<PerformanceBandUserData> userlist = new List<PerformanceBandUserData>();
                            userlist = await FetchUsers(packet, designationlist, performancebandlist, allocationType);
                            _logger.LogInformation("TreatmentOnPerformanceBand Fetch userlist " + userlist.Count());

                            var cplist = userlist.Select(a => a.CustomerPersona).ToList();

                            foreach (var usercp in cplist)
                            {
                                foreach (var cp in usercp)
                                {
                                    customerpersonaids.Add(cp);
                                }
                            }

                            personamaster = await _repoFactory.GetRepo().FindAll<UserPersonaMaster>().Where(a => customerpersonaids.Contains(a.Id)).ToListAsync();

                            /* Loop performance band list */

                            foreach (var pb in pbSubtreatmentlist)
                            {
                                List<LoanAccount> accountToSave = new List<LoanAccount>();
                                string pbPerformanceband = pb.PerformanceBand;
                                string pbCustomerPersona = pb.CustomerPersona;
                                string pbCustomerPersonaId = personamaster.Where(a => a.Name == pbCustomerPersona).Select(b => b.Id).FirstOrDefault();
                                decimal pbPercentage = !string.IsNullOrEmpty(pb.Percentage) ? Convert.ToDecimal(pb.Percentage) : 0.00m;

                                var pbuserlist = userlist.Where(a => a.PerformanceBand != null && a.CustomerPersona != null && a.PerformanceBand.Contains(pbPerformanceband) && a.CustomerPersona.Contains(pbCustomerPersonaId)).ToList();

                                _logger.LogInformation("pbPerformanceband " + pbPerformanceband);
                                _logger.LogInformation("pbCustomerPersonaId " + pbCustomerPersonaId);

                                List<LoanAccount> LoanAccount = new List<LoanAccount>();
                                List<string> AgreementIds = new List<string>();
                                AgreementIds = finalListOfLoanAccounts.Select(a => a.agreementid).ToList();

                                LoanAccount = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                        .Where(a => AgreementIds.Contains(a.AGREEMENTID) && a.CustomerPersona == pbCustomerPersona).ToListAsync();

                                if (LoanAccount.Count() > 0)
                                {
                                    List<PerformanceBandUserData> accountuserlist = new List<PerformanceBandUserData>();
                                    if (!string.IsNullOrEmpty(allocationType))
                                    {
                                        if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value))
                                        {
                                            var groupbylist = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                                .Where(b => pbuserlist.Select(d => d.AllocationId)
                                                                        .Contains(b.CollectorId) && b.CustomerPersona == pbCustomerPersona)
                                                                .GroupBy(a => a.CollectorId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                                            _logger.LogInformation("Groupbylist " + groupbylist.Count());
                                            foreach (var group in groupbylist)
                                            {
                                                PerformanceBandUserData userdetail = new PerformanceBandUserData();
                                                userdetail.AllocationId = group.KeyId;
                                                userdetail.CurrentLoad = group.Count;
                                                accountuserlist.Add(userdetail);
                                            }

                                            var exceptaccounts = LoanAccount.Where(a => userlist.Select(b => b.AllocationId).Contains(a.CollectorId)).ToList();

                                            string[] exceptarrays = exceptaccounts.Select(a => a.Id).ToArray();
                                            string[] accountsarrays = LoanAccount.Select(a => a.Id).ToArray();
                                            IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);

                                            LoanAccount = LoanAccount.Where(a => filteredlist.Contains(a.Id)).ToList();
                                        }
                                        else if (string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
                                        {
                                            var groupbylist = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                                .Where(b => pbuserlist.Select(d => d.AllocationId).Contains(b.AllocationOwnerId) && b.CustomerPersona == pbCustomerPersona)
                                                                .GroupBy(a => a.AllocationOwnerId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                                            foreach (var group in groupbylist)
                                            {
                                                PerformanceBandUserData userdetail = new PerformanceBandUserData();
                                                userdetail.AllocationId = group.KeyId;
                                                userdetail.CurrentLoad = group.Count;
                                                accountuserlist.Add(userdetail);
                                            }

                                            var exceptaccounts = LoanAccount.Where(a => userlist.Select(b => b.AllocationId).Contains(a.AllocationOwnerId)).ToList();

                                            string[] exceptarrays = exceptaccounts.Select(a => a.Id).ToArray();
                                            string[] accountsarrays = LoanAccount.Select(a => a.Id).ToArray();
                                            IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);

                                            LoanAccount = LoanAccount.Where(a => filteredlist.Contains(a.Id)).ToList();
                                        }
                                        else if (string.Equals(allocationType, "tcagent"))
                                        {
                                            var groupbylist = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                                .Where(b => pbuserlist.Select(d => d.AllocationId).Contains(b.TeleCallerId) && b.CustomerPersona == pbCustomerPersona)
                                                                .GroupBy(a => a.TeleCallerId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                                            foreach (var group in groupbylist)
                                            {
                                                PerformanceBandUserData userdetail = new PerformanceBandUserData();
                                                userdetail.AllocationId = group.KeyId;
                                                userdetail.CurrentLoad = group.Count;
                                                accountuserlist.Add(userdetail);
                                            }

                                            var exceptaccounts = LoanAccount.Where(a => userlist.Select(b => b.AllocationId).Contains(a.TeleCallerId)).ToList();

                                            string[] exceptarrays = exceptaccounts.Select(a => a.Id).ToArray();
                                            string[] accountsarrays = LoanAccount.Select(a => a.Id).ToArray();
                                            IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);

                                            LoanAccount = LoanAccount.Where(a => filteredlist.Contains(a.Id)).ToList();
                                        }
                                        else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value))
                                        {
                                            var groupbylist = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                                .Where(b => pbuserlist.Select(d => d.AllocationId).Contains(b.CollectorId) && b.CustomerPersona == pbCustomerPersona)
                                                                .GroupBy(a => a.CollectorId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                                            foreach (var group in groupbylist)
                                            {
                                                PerformanceBandUserData userdetail = new PerformanceBandUserData();
                                                userdetail.AllocationId = group.KeyId;
                                                userdetail.CurrentLoad = group.Count;
                                                accountuserlist.Add(userdetail);
                                            }

                                            var exceptaccounts = LoanAccount.Where(a => userlist.Select(b => b.AllocationId).Contains(a.CollectorId)).ToList();

                                            string[] exceptarrays = exceptaccounts.Select(a => a.Id).ToArray();
                                            string[] accountsarrays = LoanAccount.Select(a => a.Id).ToArray();
                                            IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);

                                            LoanAccount = LoanAccount.Where(a => filteredlist.Contains(a.Id)).ToList();
                                        }
                                    }

                                    pbuserlist.ForEach(a =>
                                    {
                                        var calculatedLoad = (a.MaxLoad * pbPercentage) / 100;

                                        a.PB_Load = Convert.ToInt32(Math.Floor(Convert.ToDecimal(calculatedLoad)));

                                        var user = userlist.Where(b => b.AllocationId == a.AllocationId).FirstOrDefault();

                                        if (user != null)
                                        {
                                            a.CurrentLoadBasedOnCP = user.CurrentLoad;
                                        }
                                    });

                                    //var usersids = pbuserlist.Select(a => a.AllocationId).ToArray();

                                    accountToSave = RoundRobinAllocation(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave, LoanAccount, packet.Cmd.Dto.TenantId, pbuserlist);

                                    if (accountToSave != null && accountToSave.Count() > 0)
                                    {
                                        await _treatmentCommonFunctions.ConstructDatatable(packet, accountToSave, packet.Cmd.Dto.executeTreatmentDto);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            _logger.LogInformation("Completed in TreatmentOnPerformanceBandFFPlugin ");

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<List<PerformanceBandUserData>> FetchUsers(ExecuteFragmentedTreatmentDataPacket packet, List<string> designationlist, List<string> performancebandlist, string allocationType)
        {
            List<PerformanceBandUserData> userlist = new List<PerformanceBandUserData>();
            List<string> responsibleids = new List<string>();

            #region fetch users based on allocationtype and Get CurrentLoad from  Loanaccounts table

            if (!string.IsNullOrEmpty(allocationType) && (allocationType.Equals("bankstaff") || allocationType.Equals("allocationowner")))
            {
                userlist = await _repoFactory.GetRepo().FindAll<CompanyUser>().IncludeDesignation()
                                        .IncludeCompanyUserWorkflow()
                                        .IncludeUserPerformanceBand()
                                        .IncludeUserPersona()
                                        .Where(a => a.CompanyUserWorkflowState.Name.Contains("approved") &&
                                                a.Designation.Any(b => designationlist.Contains(b.DesignationId)) &&
                                                a.userPerformanceBand.Any(b => performancebandlist.Contains(b.Name)))
                                        .Select(a => new PerformanceBandUserData()
                                        {
                                            AllocationId = a.Id,
                                            MaxLoad = a.UserLoad == null ? 0 : a.UserLoad,
                                            PerformanceBand = a.userPerformanceBand.Select(b => b.Name).ToList(),
                                            CustomerPersona = a.userCustomerPersona.Select(b => b.Name).ToList()
                                        }).ToListAsync();

                if (userlist.Count() > 0)
                {
                    var groupbyList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .Where(a => userlist.Select(b => b.AllocationId).Contains(a.CollectorId))
                                                .GroupBy(a => a.CollectorId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                    if (groupbyList != null && groupbyList.Count() > 0)
                    {
                        foreach (var group in groupbyList)
                        {
                            string collectorid = group.KeyId;
                            int groupbycount = group.Count;

                            var userperformanceband = userlist.Where(a => a.AllocationId == collectorid).FirstOrDefault();

                            userlist.Remove(userperformanceband);

                            userperformanceband.CurrentLoad = groupbycount;

                            userlist.Add(userperformanceband);
                        }
                    }
                    else if (allocationType.Equals("allocationowner"))
                    {
                        var groupbyList1 = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .Where(a => userlist.Select(b => b.AllocationId).Contains(a.AllocationOwnerId))
                                                .GroupBy(a => a.AllocationOwnerId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                        if (groupbyList1 != null && groupbyList1.Count() > 0)
                        {
                            foreach (var group in groupbyList1)
                            {
                                string collectorid = group.KeyId;
                                int groupbycount = group.Count;

                                var userperformanceband = userlist.Where(a => a.AllocationId == collectorid).FirstOrDefault();

                                userlist.Remove(userperformanceband);

                                userperformanceband.CurrentLoad = groupbycount;

                                userlist.Add(userperformanceband);
                            }
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(allocationType) && (allocationType.Equals("tcagent") || allocationType.Equals("fieldagent")))
            {
                userlist = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                            .IncludeAgencyUserDesignation()
                            .IncludeAgencyUserWorkflow().IncludeUserPerformanceBand()
                            .IncludeUserCustomerPersona()
                            .Where(a => a.AgencyUserWorkflowState.Name.Contains("approved") &&
                                a.Designation.Any(b => designationlist.Contains(b.DesignationId)) &&
                                a.userPerformanceBand.Any(b => performancebandlist.Contains(b.Name)))
                            .Select(a => new PerformanceBandUserData()
                            {
                                AllocationId = a.Id,
                                MaxLoad = a.UserLoad,
                                PerformanceBand = a.userPerformanceBand.Select(b => b.Name).ToList(),
                                CustomerPersona = a.userCustomerPersona.Select(b => b.Name).ToList()
                            }
                            ).ToListAsync();

                if (string.Equals(allocationType, "tcagent"))
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                            .Where(a => userlist.Select(c => c.AllocationId).Contains(a.ResponsibleId) &&
                                                a.AccountabilityTypeId.Contains("tc"))
                                            .ToListAsync();

                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    userlist = userlist.Where(a => responsibleids.Contains(a.AllocationId)).ToList();

                    if (userlist.Count() > 0)
                    {
                        var groupbyList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                            .Where(a => userlist.Select(b => b.AllocationId).Contains(a.TeleCallerId))
                                            .GroupBy(a => a.TeleCallerId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                        if (groupbyList != null && groupbyList.Count() > 0)
                        {
                            foreach (var group in groupbyList)
                            {
                                string telecallerid = group.KeyId;
                                int groupbycount = group.Count;

                                var userperformanceband = userlist.Where(a => a.AllocationId == telecallerid).FirstOrDefault();

                                userlist.Remove(userperformanceband);

                                userperformanceband.CurrentLoad = groupbycount;

                                userlist.Add(userperformanceband);
                            }
                        }
                    }
                }
                else if (allocationType.Equals("fieldagent"))
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                    .Where(a => userlist.Select(c => c.AllocationId).Contains(a.ResponsibleId)
                                                            && a.AccountabilityTypeId.Contains("fos"))
                                                    .ToListAsync();

                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    userlist = userlist.Where(a => responsibleids.Contains(a.AllocationId)).ToList();

                    if (userlist.Count() > 0)
                    {
                        var groupbyList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                            .Where(a => userlist.Select(b => b.AllocationId).Contains(a.CollectorId))
                                            .GroupBy(a => a.CollectorId).Select(g => new { KeyId = g.Key, Count = g.Count() }).ToListAsync();

                        if (groupbyList != null && groupbyList.Count() > 0)
                        {
                            foreach (var group in groupbyList)
                            {
                                string collectorid = group.KeyId;
                                int groupbycount = group.Count;

                                var userperformanceband = userlist.Where(a => a.AllocationId == collectorid).FirstOrDefault();

                                userlist.Remove(userperformanceband);

                                userperformanceband.CurrentLoad = groupbycount;

                                userlist.Add(userperformanceband);
                            }
                        }
                    }
                }
            }

            #endregion fetch users based on allocationtype and Get CurrentLoad from  Loanaccounts table

            return userlist;
        }

        private List<LoanAccount> RoundRobinAllocation(string TreatmentId, string allocationType, List<LoanAccount> accountToSave, List<LoanAccount> loanaccounts, string tenantid, List<PerformanceBandUserData> pbuserlist)
        {
            //var userslist = pbuserlist.Where(a => a.CurrentLoad < a.MaxLoad && a.CurrentLoadBasedOnCP < a.PB_Load).ToList();
            string[] usersIds = pbuserlist.Select(a => a.AllocationId).ToArray();
            if (pbuserlist.Count() > 0)
            {
                int useridscount = pbuserlist.Count();
                for (int j = 0; j < useridscount;)
                {
                    int countvalue = j;
                    string allocationId = usersIds[j].ToString();
                    LoanAccount acc = loanaccounts.FirstOrDefault();
                    if (acc != null)
                    {
                        var uc = pbuserlist.Where(a => a.AllocationId == allocationId).FirstOrDefault();
                        if (uc != null && uc.CurrentLoad < uc.MaxLoad && uc.CurrentLoadBasedOnCP < uc.PB_Load)
                        {
                            acc = _treatmentCommonFunctions.AssignToAccount(allocationId, acc, TreatmentId, allocationType);
                            //AssignToAccount(allocationId, acc, TreatmentId, allocationType);

                            uc.CurrentLoad++;
                            uc.CurrentLoadBasedOnCP++;

                            accountToSave.Add(acc);

                            loanaccounts.Remove(acc);
                            //pbuserlist.Add(uc);
                        }
                        else
                        {
                            pbuserlist.Remove(uc);
                            usersIds = usersIds.Where(a => a != allocationId).ToArray();
                            useridscount = pbuserlist.Count();
                        }
                    }
                    else
                    {
                        break;
                    }

                    if (countvalue == useridscount - 1)
                    {
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            return accountToSave;
        }
    }

    internal class PerformanceBandUserData
    {
        public string AllocationId { get; set; }

        public int? MaxLoad { get; set; }

        public int CurrentLoad { get; set; }

        public int PB_Load { get; set; }

        public int CurrentLoadBasedOnCP { get; set; }

        public List<string> PerformanceBand { get; set; }

        public List<string> CustomerPersona { get; set; }
    }
}