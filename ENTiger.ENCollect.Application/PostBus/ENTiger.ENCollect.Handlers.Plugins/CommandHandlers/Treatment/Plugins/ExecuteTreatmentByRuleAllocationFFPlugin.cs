using ENTiger.ENCollect.TreatmentModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;

namespace ENTiger.ENCollect
{
    public class ExecuteTreatmentByRuleAllocationFFPlugin : FlexiPluginBase,
        IFlexiPlugin<ExecuteFragmentedTreatmentDataPacket>
    {
        public override string Id { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";
        public override string FriendlyName { get; set; } = "ExecuteTreatmentAddTreatmentHistoryFFPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ExecuteTreatmentByRuleAllocationFFPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private ITreatmentCommonFunctions _treatmentCommonFunctions;
        private string hostName = string.Empty;
        private string sqlConnectionString = string.Empty;
        private string dbType = string.Empty;
        private readonly DatabaseSettings _databaseSettings;

        public ExecuteTreatmentByRuleAllocationFFPlugin(ILogger<ExecuteTreatmentByRuleAllocationFFPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory,
            ITreatmentCommonFunctions treatmentCommonFunctions, IOptions<DatabaseSettings> databaseSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _treatmentCommonFunctions = treatmentCommonFunctions;
            _databaseSettings = databaseSettings.Value;
        }

        public virtual async Task Execute(ExecuteFragmentedTreatmentDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext(); //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto.executeTreatmentDto);
            hostName = _flexAppContext.HostName;
            dbType = _databaseSettings.DBType;

            IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();

            sqlConnectionString = await _repoTenantFactory.FindAll<FlexTenantBridge>()
                                            .Where(x => x.Id == _flexAppContext.TenantId)
                                            .Select(x => x.DefaultWriteDbConnectionString).FirstOrDefaultAsync();

            if (packet.Cmd.Dto.totalCountOfAccounts > 0)
            {
                foreach (var subtreatments in packet.outputModel.subTreatment)
                {
                    if (subtreatments.TreatmentByRule.Count > 0)
                    {
                        _logger.LogInformation("Entered TreatmentByRuleAllocationFFPlugin condition");
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

                        if (DateTime.Now.Date >= subtreatmentstartdate.Value.Date &&
                            DateTime.Now.Date <= subtreatmentenddate.Value.Date)
                        {
                            _logger.LogInformation("Entered into TreatmentByRule if condition");

                            List<string> accountids = new List<string>();
                            List<ElasticSearchSimulateLoanAccountDto> finalListOfLoanAccounts =
                            new List<ElasticSearchSimulateLoanAccountDto>();
                            finalListOfLoanAccounts = await _treatmentCommonFunctions.FetchAccountsForAllocationAsync(
                                packet.Cmd.Dto.loanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TreatmentId,
                                packet.Cmd.Dto.TreatmentId, packet.Cmd.Dto.executeTreatmentDto);

                            finalListOfLoanAccounts.ForEach(a =>
                                _logger.LogInformation("finalListOfLoanAccounts " + a.agreementid));

                            if (!string.IsNullOrEmpty(subtreatments.QualifyingCondition) && string.Equals(subtreatments.QualifyingCondition, "yes"))
                            {
                                string subtreatmenid = string.Empty;
                                var sub = packet.outputModel.subTreatment
                                    .Where(a => a.Order == subtreatments.PreSubtreatmentOrder).FirstOrDefault();

                                if (sub != null)
                                {
                                    subtreatmenid = sub.Id;
                                }

                                List<string> deliverystatuses =
                                    subtreatments.DeliveryStatus.Select(a => a.Status).ToList();
                                var agreementids = await _treatmentCommonFunctions.FetchAccountsBasedOnMultipleQualifyingCondition(
                                        finalListOfLoanAccounts, subtreatments.AllocationType, packet.Cmd.Dto.TenantId,
                                        deliverystatuses, packet.Cmd.Dto.TreatmentId, subtreatmenid,
                                        packet.Cmd.Dto.TreatmentHistoryId, packet.Cmd.Dto.executeTreatmentDto);

                                accountids = finalListOfLoanAccounts.Where(a => agreementids.Contains(a.agreementid))
                                    .Select(a => a.id).ToList();
                            }
                            else
                            {
                                accountids = finalListOfLoanAccounts.Select(a => a.id).ToList();
                            }

                            List<LoanAccount> accounts = new List<LoanAccount>();
                            List<LoanAccount> filteredaccounts = new List<LoanAccount>();
                            List<LoanAccount> result = new List<LoanAccount>();

                            AndConditionCheck andconditioncheck = new AndConditionCheck();
                            List<UserWithAllCondition> userWithAllConditions = new List<UserWithAllCondition>();

                            accounts = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                .Where(a => accountids.Contains(a.Id)).ToListAsync();

                            //  result = accounts;
                            if (accounts.Count > 0)
                            {
                                string allocationType = subtreatments.AllocationType;
                                List<TreatmentLoanAccountDetails> SamePBGaccountToUpdatelist =
                                    new List<TreatmentLoanAccountDetails>();
                                List<TreatmentLoanAccountDetails> SameBranchaccountToUpdatelist =
                                    new List<TreatmentLoanAccountDetails>();
                                List<TreatmentLoanAccountDetails> SamePincodeaccountToUpdatelist =
                                    new List<TreatmentLoanAccountDetails>();
                                List<TreatmentLoanAccountDetails> StaffCodeaccountToUpdatelist =
                                    new List<TreatmentLoanAccountDetails>();
                                List<TreatmentLoanAccountDetails> finalaccountToUpdatelist =
                                    new List<TreatmentLoanAccountDetails>();

                                List<TreatmentAllocationDetail> posallocations = subtreatments.TreatmentByRule
                                    .Select(a => new TreatmentAllocationDetail { RuleName = a.Rule }).Distinct()
                                    .ToList();

                                List<string> designationlist = new List<string>();

                                if (subtreatments.Designation != null && subtreatments.Designation.Count() > 0)
                                {
                                    designationlist = subtreatments.Designation.Select(a => a.DesignationId).ToList();
                                }

                                string[] stringSeparators = new string[] { "and", "or" };
                                string[] stringruleSeparators = new string[]
                                    { "1", "2", "6", "9", "11", "12", "13", "14" };
                                string[] operatorname = posallocations.Select(a => a.RuleName).FirstOrDefault()
                                    .Split(stringruleSeparators, StringSplitOptions.None);
                                operatorname = operatorname.Where(a => a != "").ToArray();

                                string[] rulenames = posallocations.Select(a => a.RuleName).FirstOrDefault()
                                    .Split(stringSeparators, StringSplitOptions.None);

                                string ruleoperator = string.Empty;

                                int rulenamecount = rulenames.Count();

                                bool loadrule = rulenames.Contains("13") ? true : false;
                                int rulescounter = 0;
                                bool savepending = false;

                                for (int i = 0; i < rulenames.Count(); i++)
                                {
                                    int value = Convert.ToInt32(rulenames[i]);
                                    var enumrulename = (TreatmentRules)value;
                                    string rulename = enumrulename.ToString();
                                    if (i > 0 && i <= operatorname.Length)
                                    {
                                        ruleoperator = operatorname[i - 1];
                                        if (ruleoperator.IndexOf("and") >= 0)
                                        {
                                            ruleoperator = "and";
                                        }
                                        else if (ruleoperator.IndexOf("or") >= 0)
                                        {
                                            ruleoperator = "or";
                                        }
                                        else
                                        {
                                            ruleoperator = "";
                                        }
                                    }

                                    _logger.LogInformation("RuleName " + rulename);
                                    switch (rulename)
                                    {
                                        case "SamePBGasAccount":
                                            if (rulescounter == 0)
                                            {
                                                filteredaccounts = await AssignSamePBGasAccountAsync(packet, accounts,
                                                    allocationType, i, SamePBGaccountToUpdatelist, ruleoperator,
                                                    packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                    andconditioncheck, userWithAllConditions);
                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "and"))
                                            {
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> intersectlist =
                                                    accountsarrays.Intersect(filterarrays);

                                                if (intersectlist != null && intersectlist.ToList().Count > 0)
                                                {
                                                    //var andaccounts = accounts.Intersect(filteredaccounts).Select(a => a.Id).ToList();
                                                    accounts = accounts.Where(a => intersectlist.Contains(a.Id))
                                                        .ToList();
                                                    filteredaccounts = await AssignSamePBGasAccountAsync(packet, accounts,
                                                        allocationType, i, SamePBGaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                        andconditioncheck, userWithAllConditions);
                                                    savepending = true;
                                                }
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "or"))
                                            {
                                                await ConstructDatatableAsync(packet, filteredaccounts);

                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> exceptlist = accountsarrays.Except(filterarrays);

                                                if (exceptlist != null && exceptlist.ToList().Count > 0)
                                                {
                                                    accounts = accounts.Where(a => exceptlist.Contains(a.Id)).ToList();
                                                    
                                                    filteredaccounts = await AssignSamePBGasAccountAsync(packet, accounts,
                                                        allocationType, i, SamePBGaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                        andconditioncheck, userWithAllConditions);
                                                    
                                                    await ConstructDatatableAsync(packet, filteredaccounts);
                                                }

                                                savepending = false;
                                            }

                                            break;

                                        case "LatestAgency":
                                            break;

                                        case "StaffWhoHasCreatedTheLoan":

                                            if (rulescounter == 0)
                                            {
                                                filteredaccounts = await AllocateToStaffAsync(packet, accounts, allocationType,
                                                    StaffCodeaccountToUpdatelist, ruleoperator,
                                                    packet.Cmd.Dto.TreatmentId, loadrule, designationlist);
                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "and"))
                                            {
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> intersectlist =
                                                    accountsarrays.Intersect(filterarrays);

                                                if (intersectlist != null && intersectlist.ToList().Count > 0)
                                                {
                                                    //var andaccounts = accounts.Intersect(filteredaccounts).Select(a => a.Id).ToList();
                                                    accounts = accounts.Where(a => intersectlist.Contains(a.Id))
                                                        .ToList();
                                                    filteredaccounts = await AllocateToStaffAsync(packet, accounts, allocationType,
                                                        StaffCodeaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist);
                                                }

                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "or"))
                                            {
                                                await ConstructDatatableAsync(packet, filteredaccounts);

                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> exceptlist = accountsarrays.Except(filterarrays);

                                                if (exceptlist != null && exceptlist.ToList().Count > 0)
                                                {
                                                    //var oraccounts = accounts.Except(filteredaccounts).Select(a => a.Id).ToList();
                                                    accounts = accounts.Where(a => exceptlist.Contains(a.Id)).ToList();
                                                    filteredaccounts = await AllocateToStaffAsync(packet, accounts, allocationType,
                                                        StaffCodeaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist);
                                                    await ConstructDatatableAsync(packet, filteredaccounts);
                                                }

                                                savepending = false;
                                            }

                                            break;

                                        case "SameBranchAsAccount":
                                            if (rulescounter == 0)
                                            {
                                                andconditioncheck.IsBranchCondition = true;
                                                filteredaccounts = await AssignSameBranchAsAccountUsersAsync(packet, accounts,
                                                    allocationType, i, SameBranchaccountToUpdatelist, ruleoperator,
                                                    packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                    andconditioncheck, userWithAllConditions, null, null);
                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "and"))
                                            {
                                                andconditioncheck.IsBranchCondition = true;
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> intersectlist =
                                                    accountsarrays.Intersect(filterarrays);

                                                if (intersectlist != null && intersectlist.ToList().Count > 0)
                                                {
                                                    //var andaccounts = accounts.Intersect(filteredaccounts).Select(a => a.Id).ToList();
                                                    var intersectaccounts = filteredaccounts
                                                        .Where(a => intersectlist.Contains(a.Id)).ToList();
                                                    accounts = accounts.Where(a => intersectlist.Contains(a.Id))
                                                        .ToList();
                                                    filteredaccounts = await AssignSameBranchAsAccountUsersAsync(packet, accounts,
                                                        allocationType, i, SameBranchaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                        andconditioncheck, userWithAllConditions, "and",
                                                        intersectaccounts);
                                                }

                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "or"))
                                            {
                                                await ConstructDatatableAsync(packet, filteredaccounts);
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> exceptlist = accountsarrays.Except(filterarrays);

                                                if (exceptlist != null && exceptlist.ToList().Count > 0)
                                                {
                                                    accounts = accounts.Where(a => exceptlist.Contains(a.Id)).ToList();
                                                    filteredaccounts = await AssignSameBranchAsAccountUsersAsync(packet, accounts,
                                                        allocationType, i, SameBranchaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                        andconditioncheck, userWithAllConditions);
                                                    await ConstructDatatableAsync(packet, filteredaccounts);
                                                }

                                                savepending = false;
                                            }

                                            break;

                                        case "SamePincodeAsAccount":
                                            if (rulescounter == 0)
                                            {
                                                andconditioncheck.IsPincodeCondition = true;
                                                filteredaccounts = await AssignSamePincodeAsAccountUsersAsync(packet, accounts,
                                                    allocationType, i, SamePincodeaccountToUpdatelist, ruleoperator,
                                                    packet.Cmd.Dto.TenantId, loadrule, designationlist,
                                                    andconditioncheck, userWithAllConditions, null, null);
                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "and"))
                                            {
                                                andconditioncheck.IsPincodeCondition = true;
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> intersectlist =
                                                    accountsarrays.Intersect(filterarrays);

                                                if (intersectlist != null && intersectlist.ToList().Count > 0)
                                                {
                                                    var intersectaccounts = filteredaccounts
                                                        .Where(a => intersectlist.Contains(a.Id)).ToList();
                                                    //var andaccounts = accounts.Intersect(filteredaccounts).Select(a => a.Id).ToList();
                                                    accounts = accounts.Where(a => intersectlist.Contains(a.Id))
                                                        .ToList();
                                                    filteredaccounts = await AssignSamePincodeAsAccountUsersAsync(packet, accounts,
                                                        allocationType, i, SamePincodeaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                        andconditioncheck, userWithAllConditions, "and",
                                                        intersectaccounts);
                                                }

                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "or"))
                                            {
                                                await ConstructDatatableAsync(packet, filteredaccounts);
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> exceptlist = accountsarrays.Except(filterarrays);

                                                if (exceptlist != null && exceptlist.ToList().Count > 0)
                                                {
                                                    accounts = accounts.Where(a => exceptlist.Contains(a.Id)).ToList();

                                                    filteredaccounts = await AssignSamePincodeAsAccountUsersAsync(packet, accounts,
                                                        allocationType, i, SamePincodeaccountToUpdatelist, ruleoperator,
                                                        packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                        andconditioncheck, userWithAllConditions);

                                                    await ConstructDatatableAsync(packet, filteredaccounts);
                                                }

                                                savepending = false;
                                            }

                                            break;

                                        case "SamePersonaInSkillAsAccount":
                                            if (rulescounter == 0)
                                            {
                                                andconditioncheck.IsPersonaCondition = true;
                                                filteredaccounts = await AssignPersonaInSkillAsAccountUsers(packet, accounts,
                                                    allocationType, i, SamePincodeaccountToUpdatelist, ruleoperator,
                                                    packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                    andconditioncheck, userWithAllConditions);
                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "and"))
                                            {
                                                andconditioncheck.IsPersonaCondition = true;
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> intersectlist =
                                                    accountsarrays.Intersect(filterarrays);

                                                if (intersectlist != null && intersectlist.ToList().Count > 0)
                                                {
                                                    var intersectaccounts = filteredaccounts
                                                        .Where(a => intersectlist.Contains(a.Id)).ToList();

                                                    accounts = accounts.Where(a => intersectlist.Contains(a.Id))
                                                        .ToList();
                                                    filteredaccounts = await AssignPersonaInSkillAsAccountUsers(packet,
                                                        accounts, allocationType, i, SamePincodeaccountToUpdatelist,
                                                        ruleoperator, packet.Cmd.Dto.TreatmentId, loadrule,
                                                        designationlist, andconditioncheck, userWithAllConditions,
                                                        "and", intersectaccounts);
                                                }

                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "or"))
                                            {
                                                await ConstructDatatableAsync(packet, filteredaccounts);
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> exceptlist = accountsarrays.Except(filterarrays);

                                                if (exceptlist != null && exceptlist.ToList().Count > 0)
                                                {
                                                    accounts = accounts.Where(a => exceptlist.Contains(a.Id)).ToList();
                                                    
                                                    filteredaccounts = await AssignPersonaInSkillAsAccountUsers(packet,
                                                        accounts, allocationType, i, SamePincodeaccountToUpdatelist,
                                                        ruleoperator, packet.Cmd.Dto.TreatmentId, loadrule,
                                                        designationlist, andconditioncheck, userWithAllConditions);
                                                    
                                                    await ConstructDatatableAsync(packet, filteredaccounts);
                                                }

                                                savepending = false;
                                            }

                                            break;

                                        case "SameSubProductAsAccount":

                                            if (rulescounter == 0)
                                            {
                                                andconditioncheck.IsSubProductCondition = true;
                                                filteredaccounts = await AssignSameSubProductAsAccountUsersAsync(packet, accounts,
                                                    allocationType, i, SameBranchaccountToUpdatelist, ruleoperator,
                                                    packet.Cmd.Dto.TreatmentId, loadrule, designationlist,
                                                    andconditioncheck, userWithAllConditions);
                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "and"))
                                            {
                                                andconditioncheck.IsSubProductCondition = true;
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> intersectlist =
                                                    accountsarrays.Intersect(filterarrays);

                                                if (intersectlist != null && intersectlist.ToList().Count > 0)
                                                {
                                                    var intersectaccounts = filteredaccounts
                                                        .Where(a => intersectlist.Contains(a.Id)).ToList();
                                                    //var andaccounts = accounts.Intersect(filteredaccounts).Select(a => a.Id).ToList();
                                                    accounts = accounts.Where(a => intersectlist.Contains(a.Id))
                                                        .ToList();
                                                    filteredaccounts = await AssignSameSubProductAsAccountUsersAsync(packet,
                                                        accounts, allocationType, i, SameBranchaccountToUpdatelist,
                                                        ruleoperator, packet.Cmd.Dto.TreatmentId, loadrule,
                                                        designationlist, andconditioncheck, userWithAllConditions,
                                                        "and", intersectaccounts);
                                                }

                                                savepending = true;
                                            }
                                            else if (rulescounter > 0 && string.Equals(ruleoperator, "or"))
                                            {
                                                await ConstructDatatableAsync(packet, filteredaccounts);
                                                string[] filterarrays = filteredaccounts.Select(a => a.Id).ToArray();
                                                string[] accountsarrays = accounts.Select(a => a.Id).ToArray();
                                                IEnumerable<string> exceptlist = accountsarrays.Except(filterarrays);

                                                if (exceptlist != null && exceptlist.ToList().Count > 0)
                                                {
                                                    accounts = accounts.Where(a => exceptlist.Contains(a.Id)).ToList();
                                                    filteredaccounts = await AssignSameSubProductAsAccountUsersAsync(packet,
                                                        accounts, allocationType, i, SameBranchaccountToUpdatelist,
                                                        ruleoperator, packet.Cmd.Dto.TreatmentId, loadrule,
                                                        designationlist, andconditioncheck, userWithAllConditions);
                                                    await ConstructDatatableAsync(packet, filteredaccounts);
                                                }

                                                savepending = false;
                                            }

                                            break;
                                    }

                                    rulescounter++;
                                }

                                if (savepending == true)
                                {
                                    await _treatmentCommonFunctions.ConstructDatatable(packet, filteredaccounts,
                                    packet.Cmd.Dto.executeTreatmentDto);
                                }
                            }
                        }
                    }
                }
            }

            _logger.LogInformation("Completed TreatmentByRuleAllocationFFPlugin ");

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<List<LoanAccount>> AssignSamePBGasAccountAsync(ExecuteFragmentedTreatmentDataPacket packet,
            List<LoanAccount> accounts, string allocationType, int i,
            List<TreatmentLoanAccountDetails> accountToUpdateList, string ruleoperator, string tenantid, bool loadrule,
            List<string> designationList, AndConditionCheck andConditionCheck,
            List<UserWithAllCondition> userWithAllConditions)
        {
            string operatorvalue = ruleoperator;

            List<LoanAccount> AssignSamePBGasAccounts = new List<LoanAccount>();

            List<string> accountids = new List<string>();
            accountids = accounts.Select(b => b.Id).ToList();

            AssignSamePBGasAccounts = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                .Where(a => accountids.Contains(a.Id)).ToListAsync();

            var accountgroupby =
                AssignSamePBGasAccounts.GroupBy(a => new { a.PRODUCT, a.BUCKET, a.Region, a.STATE, a.CITY });
            List<LoanAccount> accountToSave = new List<LoanAccount>();

            foreach (var group in accountgroupby)
            {
                string productid = string.Empty;
                string product = group.Key.PRODUCT ?? "";
                string bucket = group.Key.BUCKET?.ToString() ?? "";
                string region = group.Key.Region ?? "";
                string state = group.Key.STATE ?? "";
                string city = group.Key.CITY ?? "";

                if (!string.Equals(product, ProductCodeEnum.All.Value))
                {
                    var categoryitem = await _repoFactory.GetRepo().FindAll<CategoryItem>()
                        .FirstOrDefaultAsync(a => string.Equals(a.Name, product));

                    if (categoryitem != null)
                    {
                        productid = categoryitem.Id;
                    }
                }
                else
                {
                    productid = ProductCodeEnum.All.Value;
                }

                List<string> allocationIds = await _treatmentCommonFunctions.FetchTreatmentAllocationIds(
                    allocationType, productid, bucket, region, state, city, packet.Cmd.Dto.TreatmentId, designationList,
                    packet.Cmd.Dto.executeTreatmentDto);

                int allocationidscount = allocationIds.Count();
                List<LoanAccount> groupedaccount = AssignSamePBGasAccounts.Where(a =>
                        string.Equals(a.PRODUCT, product) &&
                        string.Equals(a.BUCKET?.ToString(), bucket) &&
                        string.Equals(a.Region, region) &&
                        string.Equals(a.STATE, state) &&
                        string.Equals(a.CITY, city))
                    .ToList();

                string[] usersids = allocationIds.ToArray();
                accountToSave = await RoundRobinAllocationAsync(
                    packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                    groupedaccount, usersids, loadrule, tenantid, andConditionCheck, userWithAllConditions);
            }


            List<LoanAccount> PBGFilteredAccounts = new List<LoanAccount>();
            PBGFilteredAccounts = accountToSave;
            return PBGFilteredAccounts;
        }

        private async Task<List<LoanAccount>> RoundRobinAllocationAsync(string TreatmentId, string allocationType,
            List<LoanAccount> accountToSave, List<LoanAccount> loanaccounts, string[] usersIds, bool loadrule,
            string tenantid, AndConditionCheck andconditioncheck, List<UserWithAllCondition> userWithAllConditions,
            string operatorvalue = null, List<LoanAccount> filteraccounts = null)
        {
            List<UserLoadCheck> userloadchecklist = new List<UserLoadCheck>();
            if (loadrule == true)
            {
                _logger.LogInformation("RoundRobinAllocation usersIds " + usersIds.Count());

                var applicationusers = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                    .Where(a => usersIds.Contains(a.Id)).ToListAsync();

                _logger.LogInformation("applicationusers " + applicationusers.Count());
                foreach (var ausers in applicationusers)
                {
                    UserLoadCheck userload = new UserLoadCheck();
                    userload.UserId = ausers.Id;
                    userload.MaxLoad = (ausers.UserLoad == null || ausers.UserLoad == 0) ? 50 : ausers.UserLoad;
                    userloadchecklist.Add(userload);
                }

                _logger.LogInformation("userloadchecklist " + userloadchecklist.Count());

                var LoanAccounts = await _repoFactory.GetRepo().FindAll<LoanAccount>().Where(a =>
                    usersIds.Contains(a.CollectorId) || usersIds.Contains(a.TeleCallerId) ||
                    usersIds.Contains(a.AllocationOwnerId)).ToListAsync();

                userloadchecklist.ForEach(a =>
                {
                    var account = LoanAccounts.Where(b =>
                            b.CollectorId == a.UserId || b.TeleCallerId == a.UserId || b.AllocationOwnerId == a.UserId)
                        .ToList();

                    if (account.Count > 0)
                    {
                        a.CurrentLoad = account.Count;
                    }
                    else
                    {
                        a.CurrentLoad = 0;
                    }
                });
            }

            bool resetjcount = false;
            int useridscount = usersIds.Count();
            List<LoanAccount> acclist = new List<LoanAccount>();
            for (int j = 0; j < useridscount;)
            {
                int countvalue = j;
                string newallocationId = usersIds[j].ToString();
                string allocationId = usersIds[j].ToString();
                LoanAccount acc = new LoanAccount();
                if (operatorvalue == "and")
                {
                    List<string> loanaccids = loanaccounts.Select(a => a.Id).ToList();
                    List<LoanAccount> roundrobinfilteraccounts =
                        filteraccounts.Where(a => loanaccids.Contains(a.Id)).ToList();
                    if (string.Equals(allocationType, AllocationTypeEnum.TeleCallingAgency.Value))
                    {
                        roundrobinfilteraccounts.ForEach(d => { d.TeleCallingAgencyId = null; });
                        acclist = await RoundRobinFetchAccountForAllocationAsync(loanaccounts, usersIds, andconditioncheck,
                            userWithAllConditions, userloadchecklist, useridscount, newallocationId, allocationId,
                            roundrobinfilteraccounts, tenantid);
                    }
                    else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgency.Value))
                    {
                        roundrobinfilteraccounts.ForEach(d => { d.AgencyId = null; });
                        acclist = await RoundRobinFetchAccountForAllocationAsync(loanaccounts, usersIds, andconditioncheck,
                            userWithAllConditions, userloadchecklist, useridscount, newallocationId, allocationId,
                            roundrobinfilteraccounts, tenantid);
                    }
                    else if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value))
                    {
                        roundrobinfilteraccounts.ForEach(d => { d.CollectorId = null; });
                        acclist = await RoundRobinFetchAccountForAllocationAsync(loanaccounts, usersIds, andconditioncheck,
                            userWithAllConditions, userloadchecklist, useridscount, newallocationId, allocationId,
                            roundrobinfilteraccounts, tenantid);
                    }
                    else if (string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
                    {
                        roundrobinfilteraccounts.ForEach(d => { d.AllocationOwnerId = null; });
                        acclist = await RoundRobinFetchAccountForAllocationAsync(loanaccounts, usersIds, andconditioncheck,
                            userWithAllConditions, userloadchecklist, useridscount, newallocationId, allocationId,
                            roundrobinfilteraccounts, tenantid);
                    }
                    else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value))
                    {
                        roundrobinfilteraccounts.ForEach(d => { d.TeleCallerId = null; });
                        acclist = await RoundRobinFetchAccountForAllocationAsync(loanaccounts, usersIds, andconditioncheck,
                            userWithAllConditions, userloadchecklist, useridscount, newallocationId, allocationId,
                            roundrobinfilteraccounts, tenantid);
                    }
                    else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value))
                    {
                        roundrobinfilteraccounts.ForEach(d => { d.CollectorId = null; });
                        acclist = await RoundRobinFetchAccountForAllocationAsync(loanaccounts, usersIds, andconditioncheck,
                            userWithAllConditions, userloadchecklist, useridscount, newallocationId, allocationId,
                            roundrobinfilteraccounts, tenantid);
                    }

                    if (acclist.Count() > 0)
                    {
                        foreach (var acc1 in acclist)
                        {
                            LoanAccount acc2 = loanaccounts.Where(a => a.Id == acc1.Id).FirstOrDefault();

                            _logger.LogInformation("acclist LoanAccount " + acc2.AGREEMENTID);
                            _logger.LogInformation("acclist allocationId " + allocationId);
                            if (loadrule == true && !string.IsNullOrEmpty(allocationId))
                            {
                                var uc = userloadchecklist.Where(a => a.UserId == allocationId).FirstOrDefault();

                                if (uc != null && uc.CurrentLoad < uc.MaxLoad)
                                {
                                    if (!string.IsNullOrEmpty(allocationId))
                                    {
                                        acc2 = _treatmentCommonFunctions.AssignToAccount(allocationId, acc2, TreatmentId,allocationType);
                                        uc.CurrentLoad++;

                                        accountToSave.Add(acc2);
                                        loanaccounts.Remove(acc2);
                                    }
                                }
                                else
                                {
                                    userloadchecklist.Remove(uc);
                                    usersIds = usersIds.Where(a => a != newallocationId).ToArray();
                                    useridscount = userloadchecklist.Count();
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(allocationId))
                                {
                                    acc2 = _treatmentCommonFunctions.AssignToAccount(allocationId, acc2, TreatmentId,allocationType);
                                    accountToSave.Add(acc2);
                                    loanaccounts.Remove(acc2);
                                }
                            }
                            roundrobinfilteraccounts.Remove(acc2);
                        }
                    }
                    else
                    {
                        var uc = userloadchecklist.Where(a => a.UserId == newallocationId).FirstOrDefault();
                        userloadchecklist.Remove(uc);
                        usersIds = usersIds.Where(a => a != newallocationId).ToArray();
                        useridscount = userloadchecklist.Count();
                        resetjcount = true;
                        //j = 0;
                    }
                }
                else
                {
                    acc = loanaccounts.FirstOrDefault();
                }

                if (acc != null)
                {
                    if (loadrule == true && !string.IsNullOrEmpty(allocationId) && operatorvalue != "and")
                    {
                        var uc = userloadchecklist.Where(a => a.UserId == allocationId).FirstOrDefault();
                        _logger.LogInformation("uc.CurrentLoad " + uc.CurrentLoad);
                        if (uc != null && uc.CurrentLoad < uc.MaxLoad)
                        {
                            _logger.LogInformation("inside  uc.CurrentLoad < uc.MaxLoad" + uc.MaxLoad);
                            if (!string.IsNullOrEmpty(allocationId))
                            {
                                acc = _treatmentCommonFunctions.AssignToAccount(allocationId, acc, TreatmentId,
                                    allocationType);
                                uc.CurrentLoad++;
                                accountToSave.Add(acc);
                                loanaccounts.Remove(acc);
                            }
                        }
                        else
                        {
                            userloadchecklist.Remove(uc);
                            usersIds = usersIds.Where(a => a != newallocationId).ToArray();
                            useridscount = userloadchecklist.Count();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(allocationId) && operatorvalue != "and")
                        {
                            acc = _treatmentCommonFunctions.AssignToAccount(allocationId, acc, TreatmentId,
                                allocationType);
                            //AssignToAccount(allocationId, acc, TreatmentId, allocationType);
                            accountToSave.Add(acc);
                            loanaccounts.Remove(acc);
                        }
                    }
                }
                else
                {
                    break;
                }

                if (countvalue == useridscount - 1 || resetjcount == true)
                {
                    j = 0;
                }
                else
                {
                    j++;
                }
            }

            return accountToSave;
        }

        private async Task<List<LoanAccount>> RoundRobinFetchAccountForAllocationAsync(List<LoanAccount> loanaccounts, string[] usersIds,
            AndConditionCheck andconditioncheck, List<UserWithAllCondition> userWithAllConditions,
            List<UserLoadCheck> userloadchecklist, int useridscount, string newallocationId, string allocationId,
            List<LoanAccount> roundrobinfilteraccounts, string tenantid)
        {
            //LoanAccount acc = loanaccounts.FirstOrDefault();
            List<LoanAccount> acclist = new List<LoanAccount>();
            List<string> roundrobinids = new List<string>();
            roundrobinids = roundrobinfilteraccounts.Select(a => a.Id).ToList();
            // acc = roundrobinfilteraccounts.Where(a => a.CollectorId == allocationId).FirstOrDefault();
            var checkuserwithcondition = userWithAllConditions.Where(a => a.AllocationId == allocationId).ToList();
            //            bool accountnotfound = false;

            if (checkuserwithcondition != null && checkuserwithcondition.Count > 0)
            {
                List<string> pincodes = new List<string>();

                pincodes = checkuserwithcondition.Where(a => a.Pincode != null).Select(a => a.Pincode).ToList();

                List<string> branches = new List<string>();

                branches = checkuserwithcondition.Where(a => a.Branch != null).Select(a => a.Branch).ToList();

                List<string> subproduct = new List<string>();

                subproduct = checkuserwithcondition.Where(a => a.SubProduct != null).Select(a => a.SubProduct).ToList();

                List<string> personas = new List<string>();

                personas = checkuserwithcondition.Where(a => a.CustomerPersona != null).Select(a => a.CustomerPersona)
                    .ToList();

                acclist = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                    .ByTreatmentFetchLoanAccountsByPincodes(andconditioncheck.IsPincodeCondition, pincodes)
                    .ByTreatmentFetchLoanAccountsByBranch(andconditioncheck.IsBranchCondition, branches)
                    .ByTreatmentFetchLoanAccountsBySubProduct(andconditioncheck.IsSubProductCondition, subproduct)
                    .ByTreatmentFetchLoanAccountsByCustomerPersona(andconditioncheck.IsPersonaCondition, personas)
                    .Where(a => roundrobinids.Contains(a.Id)).ToListAsync();
            }
            else
            {
                var uc = userloadchecklist.Where(a => a.UserId == newallocationId).FirstOrDefault();
                userloadchecklist.Remove(uc);
                usersIds = usersIds.Where(a => a != newallocationId).ToArray();
                useridscount = userloadchecklist.Count();
            }

            return acclist;
        }

        private async Task ConstructDatatableAsync(ExecuteFragmentedTreatmentDataPacket packet, List<LoanAccount> accountToSave)
        {

            if (accountToSave.Count > 0)
            {
                if (string.Equals(dbType, "mysql"))
                {
                    foreach (var acc in accountToSave)
                    {
                        acc.SetModified();
                        acc.SetLastModifiedDate(DateTime.Now);
                        _repoFactory.GetRepo().InsertOrUpdate(acc);
                    }

                    await _repoFactory.GetRepo().SaveAsync();
                }
                else
                {
                    DataTable dt = new DataTable("TreatmentUpdateIntermediate");
                    dt.Columns.Add("Id", typeof(string));
                    dt.Columns.Add("AgreementId", typeof(string));
                    dt.Columns.Add("AllocationOwnerId", typeof(string));
                    dt.Columns.Add("TCAgencyId", typeof(string));
                    dt.Columns.Add("AgencyId", typeof(string));
                    dt.Columns.Add("TellecallerId", typeof(string));
                    dt.Columns.Add("CollectorId", typeof(string));
                    dt.Columns.Add("TreatmentId", typeof(string));
                    dt.Columns.Add("WorkRequestId", typeof(string));
                    dt.Columns.Add("IsDeleted", typeof(bool));
                    dt.Columns.Add("CreatedDate", typeof(DateTime));
                    dt.Columns.Add("LastModifiedDate", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("LastModifiedBy", typeof(string));

                    string workrequestid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                    foreach (var acc in accountToSave)
                    {
                        dt.Rows.Add(SequentialGuid.NewGuidString(), acc.AGREEMENTID, acc.AllocationOwnerId,
                            acc.TeleCallingAgencyId, acc.AgencyId, acc.TeleCallerId, acc.CollectorId,
                            packet.Cmd.Dto.TreatmentId, workrequestid, 0, DateTime.Now, DateTime.Now);
                    }

                    //fetch the factory instance
                    var utility = _flexHost.GetUtilityService<DbUtilityFactory>(hostName);

                    //fetch the correct enum w.r.t the dbType
                    var dbTypeEnum = DBTypeEnum.FromValue<DBTypeEnum>(dbType.ToLower());
                    var dbUtility = utility.GetUtility(dbTypeEnum);

                    //Call UpdateTreatmentLoanAccounts
                    UpdateTreatmentRequestDto request = new UpdateTreatmentRequestDto();
                    request.TenantId = _flexAppContext.TenantId;
                    request.Data = dt;
                    request.WorkRequestId = workrequestid;
                    await dbUtility.UpdateTreatmentLoanAccounts(request);
                }
            }
        }

        private async Task<List<LoanAccount>> AllocateToStaffAsync(ExecuteFragmentedTreatmentDataPacket packet,
            List<LoanAccount> accounts, string allocationType, List<TreatmentLoanAccountDetails> accountToUpdateList,
            string ruleoperator, string tenantid, bool loadrule, List<string> designationList)
        {
            string operatorvalue = ruleoperator;

            List<AgencyUser> agencyuserlist = new List<AgencyUser>();
            List<AgencyUser> fieldagencyuserlist = new List<AgencyUser>();
            List<CompanyUser> companyuserlist = new List<CompanyUser>();
            List<LoanAccount> accountToSave = new List<LoanAccount>();
            List<string> responsibleids = new List<string>();

            List<LoanAccount> AssignStaffCodeAsAccountUsers = new List<LoanAccount>();
            List<string> accountids = new List<string>();
            accountids = accounts.Select(b => b.Id).ToList();

            AssignStaffCodeAsAccountUsers = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                .Where(a => accountids.Contains(a.Id)).ToListAsync();

            if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value))
            {
                agencyuserlist = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                .IncludeAgencyUserWorkflow()
                                .IncludeAgencyUserDesignation()
                                .Where(a => a.AgencyUserWorkflowState != null &&
                                            a.AgencyUserWorkflowState.Name.IndexOf("approved") >= 0 &&
                                            a.Designation.Any(b => designationList.Contains(b.DesignationId))
                                ).ToListAsync();

                var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                        .Where(a => agencyuserlist.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                                    a.AccountabilityTypeId.IndexOf("tc") >= 0)
                                        .ToListAsync();


                responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                agencyuserlist = agencyuserlist.Where(a => responsibleids.Contains(a.Id)).ToList();

                if (agencyuserlist.Count() > 0)
                {
                    foreach (var acc in AssignStaffCodeAsAccountUsers)
                    {
                        string salespointcode = ""; //acc.SalesPointCode;
                        var allocation = agencyuserlist.Where(a => a.CustomId == salespointcode).FirstOrDefault();
                        if (allocation != null)
                        {
                            AssignToAccount(allocation.Id.ToString(), acc, packet.Cmd.Dto.TreatmentId, allocationType);
                        }

                        accountToSave.Add(acc);
                    }
                }
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value))
            {
                fieldagencyuserlist = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                                .IncludeAgencyUserDesignation()
                                                .IncludeAgencyUserWorkflow()
                                                .Where(a =>
                                                        a.AgencyUserWorkflowState != null &&
                                                        a.AgencyUserWorkflowState.Name.IndexOf("approved") >= 0 &&
                                                        a.Designation.Any(b => designationList.Contains(b.DesignationId))
                                                ).ToListAsync();

                var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                .Where(a => fieldagencyuserlist.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                                            a.AccountabilityTypeId.IndexOf("fos") >= 0)
                                                .ToListAsync();

                responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                fieldagencyuserlist = fieldagencyuserlist.Where(a => responsibleids.Contains(a.Id)).ToList();

                if (fieldagencyuserlist.Count() > 0)
                {
                    foreach (var acc in AssignStaffCodeAsAccountUsers)
                    {
                        string salespointcode = ""; // acc.SalesPointCode;
                        var allocation = fieldagencyuserlist.Where(a => a.CustomId == salespointcode).FirstOrDefault();
                        if (allocation != null)
                        {
                            AssignToAccount(allocation.Id.ToString(), acc, packet.Cmd.Dto.TreatmentId, allocationType);

                            accountToSave.Add(acc);
                        }
                    }
                }
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value) || string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
            {
                companyuserlist = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                            .IncludeCompanyUserWorkflow()
                                            .IncludeDesignation()
                                            .Where(a =>
                                                    a.CompanyUserWorkflowState != null &&
                                                    a.CompanyUserWorkflowState.Name.IndexOf("approved") >= 0 &&
                                                    a.Designation.Any(b => designationList.Contains(b.DesignationId))
                                            ).ToListAsync();

                if (companyuserlist.Count() > 0)
                {
                    foreach (var acc in AssignStaffCodeAsAccountUsers)
                    {
                        string salespointcode = ""; // acc.SalesPointCode;
                        var allocation = companyuserlist.Where(a => a.CustomId == salespointcode).FirstOrDefault();
                        if (allocation != null)
                        {
                            AssignToAccount(allocation.Id.ToString(), acc, packet.Cmd.Dto.TreatmentId, allocationType);

                            accountToSave.Add(acc);
                        }
                    }
                }
            }

            List<LoanAccount> AllocFilteredAccounts = new List<LoanAccount>();
            AllocFilteredAccounts = accountToSave;

            return AllocFilteredAccounts;
        }

        private void AssignToAccount(string allocationId, LoanAccount acc, string TreatmentId, string allocationType)
        {
            try
            {
                if (string.Equals(allocationType, AllocationTypeEnum.TeleCallingAgency.Value))
                {
                    acc.TeleCallingAgencyId = allocationId;
                    acc.TeleCaller = null;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgency.Value))
                {
                    acc.AgencyId = allocationId;
                    acc.CollectorId = null;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value) ||
                         string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
                {
                    acc.AllocationOwnerId = allocationId;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value))
                {
                    acc.TeleCallerId = allocationId;
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value))
                {
                    acc.CollectorId = allocationId;
                }

                // Common updates
                acc.TreatmentId = TreatmentId;
                acc.SetLastModifiedDate(DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in AssignToAccount: {ex}");
            }
        }


        private async Task<List<LoanAccount>> AssignSameBranchAsAccountUsersAsync(ExecuteFragmentedTreatmentDataPacket packet,
            List<LoanAccount> accounts, string allocationType, int i,
            List<TreatmentLoanAccountDetails> accountToUpdateList, string ruleoperator, string tenantid, bool loadrule,
            List<string> designationlist, AndConditionCheck andConditionCheck,
            List<UserWithAllCondition> userWithAllCondition, string operatorvalue = null,
            List<LoanAccount> filteredaccounts = null)
        {
            List<LoanAccount> AssignSameBranchAsAccountUsers = new List<LoanAccount>();
            List<string> accountids = new List<string>();
            accountids = accounts.Select(b => b.Id).ToList();

            AssignSameBranchAsAccountUsers = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                .Where(a => accountids.Contains(a.Id)).ToListAsync();

            List<string> branches = AssignSameBranchAsAccountUsers.Select(a => a.BRANCH.ToLower()).Distinct().ToList();
            List<TreatmentUsersName> treatmentusernamelist = new List<TreatmentUsersName>();
            List<LoanAccount> accountToSave = new List<LoanAccount>();
            if (string.Equals(allocationType, "bankstaff") || string.Equals(allocationType, "allocationowner"))
            {
                List<CompanyUser> users = new List<CompanyUser>();

                if (operatorvalue == "and")
                {
                    if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value))
                    {
                        List<string> bankstaffids = new List<string>();
                        bankstaffids = userWithAllCondition.Select(a => a.AllocationId).ToList();

                        _logger.LogInformation("branch bankstaffids" + bankstaffids.Count());

                        _logger.LogInformation("Subproduct bankstaffids list " + bankstaffids.ToList());

                        users = await _repoFactory.GetRepo().FindAll<CompanyUser>().IncludeUserGeoScope()
                                .IncludeCompanyUserWorkflow().IncludeDesignation()
                                .Where(a => bankstaffids.Contains(a.Id) && a.CompanyUserWorkflowState != null &&
                                            a.GeoScopes.Any(b =>
                                                b.IsDeleted == false && branches.Contains(b.GeoScopeId)) &&
                                            string.Equals(a.CompanyUserWorkflowState.Name, "approved")
                                ).ToListAsync();

                    }
                    if (allocationType.Equals(AllocationTypeEnum.AllocationOwner.Value))
                    {
                        List<string> allocationownerids = filteredaccounts.Select(a => a.AllocationOwnerId).ToList();

                        users = await _repoFactory.GetRepo().FindAll<CompanyUser>().IncludeUserGeoScope()
                            .IncludeCompanyUserWorkflow().IncludeDesignation()
                            .Where(a => allocationownerids.Contains(a.Id) && a.CompanyUserWorkflowState != null &&
                                        a.GeoScopes.Any(b => branches.Contains(b.GeoScopeId)) &&
                                        string.Equals(a.CompanyUserWorkflowState.Name, "approved")
                            ).ToListAsync();
                    }

                }
                else
                {
                    users = await _repoFactory.GetRepo().FindAll<CompanyUser>().IncludeUserGeoScope()
                                .IncludeCompanyUserWorkflow().IncludeDesignation()
                                .Where(a => a.CompanyUserWorkflowState != null
                                    && a.GeoScopes.Any(b => b.IsDeleted == false
                                    && branches.Contains(b.GeoScopeId))
                                    && string.Equals(a.CompanyUserWorkflowState.Name, "approved")
                          ).ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    users = users.Where(a => a.Designation.Any(b => designationlist.Contains(b.DesignationId)))
                        .ToList();
                }

                foreach (var user in users)
                {
                    var brancheslist = user.GeoScopes.Where(a => a.IsDeleted == false && a.GeoScopeId != null)
                        .Select(a => a.GeoScopeId).Distinct().ToList();
                    foreach (var branch in brancheslist)
                    {
                        TreatmentUsersName user1 = new TreatmentUsersName();
                        user1.AllocationId = user.Id;
                        user1.Branch = branch;
                        treatmentusernamelist.Add(user1);

                        //var fetchuser = userWithAllCondition.Where(a => a.AllocationId == user1.AllocationId).FirstOrDefault();

                        //if(fetchuser != null)
                        //{
                        //    fetchuser.Branch = branch;
                        //    userWithAllCondition.Add(fetchuser);
                        //}
                        //else
                        //{
                        UserWithAllCondition branchuser = new UserWithAllCondition();
                        branchuser.AllocationId = user1.AllocationId;
                        branchuser.Branch = user1.Branch;
                        userWithAllCondition.Add(branchuser);

                        //}
                    }
                }

                foreach (var branchvalue in branches)
                {
                    List<LoanAccount> branchaccounts = new List<LoanAccount>();
                    branchaccounts = AssignSameBranchAsAccountUsers
                        .Where(a => string.Equals(a.BRANCH, branchvalue))  // Used String.Equals for case-insensitive comparison
                        .ToList();

                    var users1 = treatmentusernamelist
                        .Where(a => string.Equals(a.Branch, branchvalue))  // Used String.Equals for case-insensitive comparison
                        .ToArray();


                    int useridscount = users1.Count();

                    string[] usersids = users1.Select(a => a.AllocationId).ToArray();

                    if (useridscount > 0)
                    {
                        accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                            branchaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllCondition,
                            operatorvalue, filteredaccounts);
                    }
                }
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value))
            {
                List<string> responsibleids = new List<string>();
                List<AgencyUser> agencyusers = new List<AgencyUser>();

                if (operatorvalue == "and")
                {
                    List<string> tellecallerids = new List<string>();
                    tellecallerids = filteredaccounts.Select(a => a.TeleCallerId).ToList();
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserGeoScope()
                                  .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                  .Where(a => tellecallerids.Contains(a.Id) && a.AgencyUserWorkflowState != null &&
                                              a.GeoScopes.Any(b => branches.Contains(b.GeoScopeId)) &&
                                              string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                  ).ToListAsync();

                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserGeoScope()
                                 .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                 .Where(a => a.AgencyUserWorkflowState != null 
                                        && a.GeoScopes.Any(b => branches.Contains(b.GeoScopeId)) 
                                        && string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                 ).ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    agencyusers = agencyusers
                        .Where(a => a.Designation.Any(b => designationlist.Contains(b.DesignationId))).ToList();
                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a =>
                                           agencyusers.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                           a.AccountabilityTypeId != null &&
                                           a.AccountabilityTypeId.IndexOf("tc") >= 0).ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var agencyuser in agencyusers)
                    {
                        var brancheslist = agencyuser.GeoScopes.Where(a => a.GeoScopeId != null).Select(a => a.GeoScope.Item)
                            .Distinct().ToList();
                        foreach (var branch in brancheslist)
                        {
                            TreatmentUsersName user1 = new TreatmentUsersName();
                            user1.AllocationId = agencyuser.Id;
                            user1.Branch = branch;
                            treatmentusernamelist.Add(user1);

                            var fetchuser = userWithAllCondition.Where(a => a.AllocationId == user1.AllocationId)
                                .FirstOrDefault();

                            if (fetchuser != null)
                            {
                                fetchuser.Branch = branch;
                                userWithAllCondition.Add(fetchuser);
                            }
                            else
                            {
                                UserWithAllCondition branchuser = new UserWithAllCondition();
                                branchuser.AllocationId = user1.AllocationId;
                                branchuser.Branch = user1.Branch;
                                userWithAllCondition.Add(branchuser);
                            }
                        }
                    }
                }

                foreach (var branchvalue in branches)
                {
                    List<LoanAccount> branchaccounts = new List<LoanAccount>();
                    branchaccounts = AssignSameBranchAsAccountUsers.Where(a => string.Equals(a.BRANCH, branchvalue)).ToList();

                    var users = treatmentusernamelist.Where(a => string.Equals(a.Branch, branchvalue)).ToArray();


                    string[] usersids = users.Select(a => a.AllocationId).ToArray();

                    int useridscount = users.Count();
                    accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                        branchaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllCondition);
                }
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value))
            {
                List<string> responsibleids = new List<string>();
                List<AgencyUser> agencyusers = new List<AgencyUser>();

                if (operatorvalue == "and")
                {
                    List<string> fieldagentids = new List<string>();
                    fieldagentids = filteredaccounts.Select(a => a.CollectorId).ToList();

                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserGeoScope()
                                  .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                  .Where(a => fieldagentids.Contains(a.Id) && a.AgencyUserWorkflowState != null &&
                                              a.GeoScopes.Any(b => branches.Contains(b.GeoScopeId)) &&
                                              string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                  ).ToListAsync();

                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserGeoScope()
                                 .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                 .Where(a => a.AgencyUserWorkflowState != null &&
                                             a.GeoScopes.Any(b => branches.Contains(b.GeoScopeId)) &&
                                             string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                 ).ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    agencyusers = agencyusers
                        .Where(a => a.Designation.Any(b => designationlist.Contains(b.DesignationId))).ToList();
                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                        .Where(a => agencyusers.Select(c => c.Id).Contains(a.ResponsibleId)
                                                            && string.Equals(a.AccountabilityTypeId, "fos")).ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var agencyuser in agencyusers)
                    {
                        var brancheslist = agencyuser.GeoScopes.Where(a => a.GeoScope != null).Select(a => a.GeoScope.Item)
                            .Distinct().ToList();
                        foreach (var branch in brancheslist)
                        {
                            TreatmentUsersName user1 = new TreatmentUsersName();
                            user1.AllocationId = agencyuser.Id;
                            user1.Branch = branch;
                            treatmentusernamelist.Add(user1);

                            var fetchuser = userWithAllCondition.Where(a => a.AllocationId == user1.AllocationId)
                                .FirstOrDefault();

                            if (fetchuser != null)
                            {
                                fetchuser.Branch = branch;
                                userWithAllCondition.Add(fetchuser);
                            }
                            else
                            {
                                UserWithAllCondition branchuser = new UserWithAllCondition();
                                branchuser.AllocationId = user1.AllocationId;
                                branchuser.Branch = user1.Branch;
                                userWithAllCondition.Add(branchuser);
                            }
                        }
                    }
                }

                foreach (var branchvalue in branches)
                {
                    List<LoanAccount> branchaccounts = new List<LoanAccount>();
                    branchaccounts = AssignSameBranchAsAccountUsers.Where(a => string.Equals(a.BRANCH, branchvalue)).ToList();


                    var users = treatmentusernamelist.Where(a => string.Equals(a.Branch, branchvalue)).ToArray();


                    string[] usersids = users.Select(a => a.AllocationId).ToArray();

                    int useridscount = users.Count();
                    accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                        branchaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllCondition,
                        operatorvalue, filteredaccounts);
                }
            }

            List<LoanAccount> SamebranchFilteredAccounts = new List<LoanAccount>();
            SamebranchFilteredAccounts = accountToSave;
            return SamebranchFilteredAccounts;
        }

        private async Task<List<LoanAccount>> AssignSamePincodeAsAccountUsersAsync(ExecuteFragmentedTreatmentDataPacket packet,
            List<LoanAccount> accounts, string allocationType, int i,
            List<TreatmentLoanAccountDetails> accountToUpdateList, string ruleoperator, string tenantid, bool loadrule,
            List<string> designationlist, AndConditionCheck andConditionCheck,
            List<UserWithAllCondition> userWithAllConditions, string operatorvalue = null,
            List<LoanAccount> filteredaccounts = null)
        {
            List<LoanAccount> AssignSamePincodeAsAccountUsers = new List<LoanAccount>();
            List<string> accountids = new List<string>();
            accountids = accounts.Select(b => b.Id).ToList();

            AssignSamePincodeAsAccountUsers = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                .Where(a => accountids.Contains(a.Id)).ToListAsync();

            List<string> mailingzipcodelist = AssignSamePincodeAsAccountUsers.Select(a => a.MAILINGZIPCODE.ToLower())
                .Distinct().ToList();
            List<TreatmentPincodeUsersName> treatmentpincodeusernamelist = new List<TreatmentPincodeUsersName>();
            List<LoanAccount> accountToSave = new List<LoanAccount>();

            if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value) || string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
            {

                List<string> responsibleids = new List<string>();
                List<CompanyUserPOW> companyuserpowlist = new List<CompanyUserPOW>();
                List<CompanyUser> companyusers = new List<CompanyUser>();

                if (operatorvalue == "and")
                {
                    if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value))
                    {
                        List<string> bankstaffids = new List<string>();
                        bankstaffids = userWithAllConditions.Select(a => a.AllocationId).ToList();

                        companyusers = await _repoFactory.GetRepo().FindAll<CompanyUser>().CompanyUserIncludePlaceOfWork().IncludeCompanyUserWorkflow().IncludeDesignation()
                                      .Where(a => bankstaffids.Contains(a.Id) && a.CompanyUserWorkflowState != null &&
                                                  string.Equals(a.CompanyUserWorkflowState.Name, "approved")).ToListAsync();

                    }

                    if (string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
                    {
                        List<string> allocationownerids = new List<string>();
                        allocationownerids = filteredaccounts.Select(a => a.AllocationOwnerId).ToList();

                        companyusers = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                            .CompanyUserIncludePlaceOfWork()
                                            .IncludeCompanyUserWorkflow()
                                            .IncludeDesignation()
                                            .Where(a => allocationownerids.Contains(a.Id)
                                                  && a.CompanyUserWorkflowState != null
                                                  && string.Equals(a.CompanyUserWorkflowState.Name, "approved"))
                                            .ToListAsync();

                    }
                }
                else
                {
                    companyusers = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                            .CompanyUserIncludePlaceOfWork()
                                            .IncludeCompanyUserWorkflow()
                                            .IncludeDesignation()
                                            .Where(a => a.CompanyUserWorkflowState != null
                                                        && a.CompanyUserWorkflowState.Name != null
                                                        && a.CompanyUserWorkflowState.Name.Contains("approved"))
                                            .ToListAsync();

                }

                _logger.LogInformation("Companyuser count " + companyusers.Count());
                if (designationlist.Count() > 0)
                {
                    companyusers = companyusers.Where(a =>
                            a.Designation != null && a.Designation.Any(b => designationlist.Contains(b.DesignationId)))
                        .ToList();
                    _logger.LogInformation("Companyuser count after designation" + companyusers.Count());
                }

                _logger.LogInformation("BankStff users count " + companyusers.Count());
                if (companyusers.Count() > 0)
                {
                    foreach (var cusers in companyusers)
                    {
                        if (cusers.PlaceOfWork != null)
                        {
                            var powareacode = cusers.PlaceOfWork.Where(a => a.IsDeleted == false).ToList();

                            foreach (var areacode in powareacode)
                            {
                                CompanyUserPOW companyuserpow = new CompanyUserPOW();
                                _logger.LogInformation("Companyuserid " + areacode.CompanyUserId);
                                var areacodes = areacode.PIN;
                                _logger.LogInformation("AreaCode of companyuser " + areacodes);

                                if (areacodes != null)
                                {
                                    //    var splitareacodes = areacodes.Split(',').ToList();

                                    //foreach (var split in splitareacodes)
                                    //{
                                    companyuserpow.Id = cusers.Id;
                                    companyuserpow.AreaCode = areacodes.ToString();
                                    companyuserpowlist.Add(companyuserpow);
                                    //}
                                }
                            }
                        }
                    }

                    foreach (var companyuser in companyuserpowlist)
                    {
                        TreatmentPincodeUsersName user1 = new TreatmentPincodeUsersName();
                        user1.AllocationId = companyuser.Id;
                        user1.AreaCode = companyuser.AreaCode;
                        treatmentpincodeusernamelist.Add(user1);

                        //var fetchuser = userWithAllConditions.Where(a => a.AllocationId == user1.AllocationId).FirstOrDefault();

                        //if (fetchuser != null)
                        //{
                        //    fetchuser.Pincode = companyuser.AreaCode;
                        //    userWithAllConditions.Add(fetchuser);
                        //}
                        //else
                        //{
                        UserWithAllCondition branchuser = new UserWithAllCondition();
                        branchuser.AllocationId = user1.AllocationId;
                        branchuser.Pincode = companyuser.AreaCode;
                        userWithAllConditions.Add(branchuser);

                        //}
                    }
                }

                await PincodeAllocateToUserAsync(packet, allocationType, accountToUpdateList, tenantid,
                    AssignSamePincodeAsAccountUsers, mailingzipcodelist, treatmentpincodeusernamelist, accountToSave,
                    loadrule, operatorvalue, filteredaccounts, andConditionCheck, userWithAllConditions);
            }
            else if (string.Equals(allocationType, "tcagent"))
            {
                List<string> responsibleids = new List<string>();
                List<AgencyUserPOW> agencyuserpowlist = new List<AgencyUserPOW>();

                List<AgencyUser> agencyusers = new List<AgencyUser>();

                if (operatorvalue == "and")
                {
                    List<string> tellecallerids = new List<string>();
                    tellecallerids = filteredaccounts.Select(a => a.TeleCallerId).ToList();
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                            .IncludeAgencyUserPlaceOfWork()
                                            .IncludeAgencyUserWorkflow()
                                            .IncludeAgencyUserDesignation()
                                            .Where(a => tellecallerids.Contains(a.Id) && a.AgencyUserWorkflowState != null &&
                                                          string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                            ).ToListAsync();

                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                                .IncludeAgencyUserPlaceOfWork()
                                                .IncludeAgencyUserWorkflow()
                                                .IncludeAgencyUserDesignation()
                                                .Where(a => a.AgencyUserWorkflowState != null &&
                                                          a.AgencyUserWorkflowState.Name != null &&
                                                          a.AgencyUserWorkflowState.Name.Contains("approved")
                                                ).ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    agencyusers = agencyusers.Where(a =>
                            a.Designation != null && a.Designation.Any(b => designationlist.Contains(b.DesignationId)))
                        .ToList();
                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                .Where(a => agencyusers.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                                    a.AccountabilityTypeId != null &&
                                                    a.AccountabilityTypeId.Contains("tc")
                                                ).ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var ausers in agencyusers)
                    {
                        AgencyUserPOW agencyuserpow = new AgencyUserPOW();

                        if (ausers.PlaceOfWork != null)
                        {
                            var areacode = ausers.PlaceOfWork.FirstOrDefault();

                            if (areacode != null && areacode.PIN != null)
                            {
                                var areacodes = areacode.PIN.ToString();
                                _logger.LogInformation("AreaCode of companyuser " + areacodes);
                                if (areacodes != null)
                                {
                                    var splitareacodes = areacodes.Split(',').ToList();

                                    foreach (var split in splitareacodes)
                                    {
                                        agencyuserpow.Id = ausers.Id;
                                        agencyuserpow.AreaCode = split.ToString();
                                        agencyuserpowlist.Add(agencyuserpow);
                                    }
                                }
                            }
                        }
                    }

                    foreach (var agencyuser in agencyuserpowlist)
                    {
                        TreatmentPincodeUsersName user1 = new TreatmentPincodeUsersName();
                        user1.AllocationId = agencyuser.Id;
                        user1.AreaCode = agencyuser.AreaCode;
                        treatmentpincodeusernamelist.Add(user1);

                        UserWithAllCondition branchuser = new UserWithAllCondition();
                        branchuser.AllocationId = user1.AllocationId;
                        branchuser.Pincode = agencyuser.AreaCode;
                        userWithAllConditions.Add(branchuser);
                    }
                }

                await PincodeAllocateToUserAsync(packet, allocationType, accountToUpdateList, tenantid,
                    AssignSamePincodeAsAccountUsers, mailingzipcodelist, treatmentpincodeusernamelist, accountToSave,
                    loadrule, operatorvalue, filteredaccounts, andConditionCheck, userWithAllConditions);
            }
            else if (string.Equals(allocationType, "fieldagent"))
            {
                List<string> responsibleids = new List<string>();
                List<AgencyUserPOW> agencyuserpowlist = new List<AgencyUserPOW>();
                List<AgencyUser> agencyusers = new List<AgencyUser>();

                if (operatorvalue == "and")
                {
                    List<string> fieldagentids = filteredaccounts.Select(a => a.CollectorId).ToList();
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                            .IncludeAgencyUserPlaceOfWork()
                                            .IncludeAgencyUserWorkflow()
                                            .IncludeAgencyUserDesignation()
                                            .Where(a => fieldagentids.Contains(a.Id) &&
                                                        a.AgencyUserWorkflowState != null &&
                                                        string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                            ).ToListAsync();

                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                              .IncludeAgencyUserPlaceOfWork()
                                              .IncludeAgencyUserWorkflow()
                                              .IncludeAgencyUserDesignation()
                                              .Where(a => a.AgencyUserWorkflowState != null &&
                                                          a.AgencyUserWorkflowState.Name.Contains("approved")
                                              ).ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    agencyusers = agencyusers.Where(a =>
                            a.Designation != null && a.Designation.Any(b => designationlist.Contains(b.DesignationId)))
                        .ToList();
                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                .Where(a => agencyusers.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                                        a.AccountabilityTypeId.Contains("fos"))
                                                .ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var ausers in agencyusers)
                    {
                        AgencyUserPOW agencyuserpow = new AgencyUserPOW();
                        if (ausers.PlaceOfWork != null)
                        {
                            var areacode = ausers.PlaceOfWork.FirstOrDefault();

                            if (areacode != null && areacode.PIN != null)
                            {
                                var areacodes = areacode.PIN.ToString();
                                _logger.LogInformation("AreaCode of companyuser " + areacodes);
                                if (areacodes != null)
                                {
                                    var splitareacodes = areacodes.Split(',').ToList();

                                    foreach (var split in splitareacodes)
                                    {
                                        agencyuserpow.Id = ausers.Id;
                                        agencyuserpow.AreaCode = split.ToString();
                                        agencyuserpowlist.Add(agencyuserpow);
                                    }
                                }
                            }
                        }
                    }

                    foreach (var agencyuser in agencyuserpowlist)
                    {
                        TreatmentPincodeUsersName user1 = new TreatmentPincodeUsersName();
                        user1.AllocationId = agencyuser.Id;
                        user1.AreaCode = agencyuser.AreaCode;
                        treatmentpincodeusernamelist.Add(user1);

                        //var fetchuser = userWithAllConditions.Where(a => a.AllocationId == user1.AllocationId).FirstOrDefault();

                        //if (fetchuser != null)
                        //{
                        //    fetchuser.Pincode = agencyuser.AreaCode;
                        //    userWithAllConditions.Add(fetchuser);
                        //}
                        //else
                        //{
                        UserWithAllCondition branchuser = new UserWithAllCondition();
                        branchuser.AllocationId = user1.AllocationId;
                        branchuser.Pincode = agencyuser.AreaCode;
                        userWithAllConditions.Add(branchuser);

                        //}
                    }
                }

                await PincodeAllocateToUserAsync(packet, allocationType, accountToUpdateList, tenantid,
                    AssignSamePincodeAsAccountUsers, mailingzipcodelist, treatmentpincodeusernamelist, accountToSave,
                    loadrule, operatorvalue, filteredaccounts, andConditionCheck, userWithAllConditions);
            }

            List<LoanAccount> PincodeFilteredAccounts = new List<LoanAccount>();
            PincodeFilteredAccounts = accountToSave;
            return PincodeFilteredAccounts;
        }

        private async Task PincodeAllocateToUserAsync(ExecuteFragmentedTreatmentDataPacket packet, string allocationType,
            List<TreatmentLoanAccountDetails> accountToUpdateList, string tenantid,
            List<LoanAccount> AssignSamePincodeAsAccountUsers, List<string> mailingzipcodelist,
            List<TreatmentPincodeUsersName> treatmentpincodeusernamelist, List<LoanAccount> accountToSave,
            bool loadrule, string operatorvalue, List<LoanAccount> filteredaccounts,
            AndConditionCheck andConditionCheck, List<UserWithAllCondition> userWithAllCondition)
        {
            foreach (var mailingzipcode in mailingzipcodelist)
            {
                _logger.LogInformation("treatmentpincodeusernamelist " + treatmentpincodeusernamelist.Count());

                _logger.LogInformation("mailingzipcode " + mailingzipcode);
                List<LoanAccount> pincodeaccounts = new List<LoanAccount>();
                pincodeaccounts = AssignSamePincodeAsAccountUsers.Where(a => string.Equals(a.MAILINGZIPCODE, mailingzipcode)).ToList();

                var users = treatmentpincodeusernamelist.Where(a => a.AreaCode == mailingzipcode).ToArray();

                string[] usersids = users.Select(a => a.AllocationId).Distinct().ToArray();
                _logger.LogInformation("usersids " + usersids.Count());
                if (usersids.Count() > 0)
                {
                    accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                        pincodeaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllCondition,
                        operatorvalue, filteredaccounts);
                }
            }
        }

        private async Task<List<LoanAccount>> AssignSameSubProductAsAccountUsersAsync(ExecuteFragmentedTreatmentDataPacket packet,
            List<LoanAccount> accounts, string allocationType, int i,
            List<TreatmentLoanAccountDetails> accountToUpdateList, string ruleoperator, string tenantid, bool loadrule,
            List<string> designationlist, AndConditionCheck andConditionCheck,
            List<UserWithAllCondition> userWithAllConditions, string operatorvalue = null,
            List<LoanAccount> filteredaccounts = null)
        {
            List<LoanAccount> AssignSameSubProductAsAccountUsers = new List<LoanAccount>();
            List<LoanAccount> accountToSave = new List<LoanAccount>();

            List<string> accids = new List<string>();
            accids = accounts.Select(b => b.Id).ToList();

            AssignSameSubProductAsAccountUsers = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                            .Where(a => accids.Contains(a.Id)).ToListAsync();

            List<string> subproduct = AssignSameSubProductAsAccountUsers.Select(a => a.SubProduct).Distinct().ToList();

            var subproductidandnames = await _repoFactory.GetRepo().FindAll<CategoryItem>()
                                                .Where(a => string.Equals(a.CategoryMasterId, CategoryMasterEnum.ImportAccountsHeaders.Value)
                                                    && subproduct.Contains(a.Name))
                                                .Select(a => new { SubproductId = a.Id, SubproductName = a.Name }).ToListAsync();


            List<string> subproductids = subproductidandnames.Select(a => a.SubproductId).ToList();
            List<TreatmentUsersSubProductName> treatmentusernamelist = new List<TreatmentUsersSubProductName>();

            if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value) || string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
            {

                List<CompanyUser> users = new List<CompanyUser>();

                if (operatorvalue == "and")
                {
                    if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value))
                    {
                        List<string> bankstaffids = new List<string>();
                        bankstaffids = userWithAllConditions.Select(a => a.AllocationId).Distinct().ToList();

                        _logger.LogInformation("Subproduct bankstaffids" + bankstaffids.Count());

                        _logger.LogInformation("Subproduct bankstaffids list " + bankstaffids.AsJson());
                        users = await _repoFactory.GetRepo().FindAll<CompanyUser>().IncludeUserProductScope()
                                        .IncludeCompanyUserWorkflow().IncludeDesignation()
                                        .Where(a => bankstaffids.Contains(a.Id) && a.CompanyUserWorkflowState != null &&
                                            a.ProductScopes.Any(b =>
                                               !b.IsDeleted && subproductids.Contains(b.ProductScopeId)) &&
                                                string.Equals(a.CompanyUserWorkflowState.Name, "approved"))
                                        .ToListAsync();

                    }

                    if (string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
                    {
                        List<string> allocationownerids = new List<string>();
                        allocationownerids = filteredaccounts.Select(a => a.AllocationOwnerId).ToList();
                        users = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                        .IncludeUserProductScope()
                                        .IncludeCompanyUserWorkflow()
                                        .IncludeDesignation()
                                        .Where(a => allocationownerids.Contains(a.Id) &&
                                                    a.CompanyUserWorkflowState != null &&
                                                    a.ProductScopes.Any(b => subproductids.Contains(b.ProductScopeId) || string.Equals(b.ProductScopeId, "All")) &&
                                                    string.Equals(a.CompanyUserWorkflowState.Name, "approved"))
                                        .ToListAsync();
                    }
                }
                else
                {
                    users = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                          .IncludeUserProductScope()
                          .IncludeCompanyUserWorkflow()
                          .IncludeDesignation()
                          .Where(a => a.CompanyUserWorkflowState != null &&
                                      a.ProductScopes.Any(b =>
                                          b.IsDeleted == false &&
                                          (subproductids.Contains(b.ProductScopeId) ||
                                           string.Equals(b.ProductScopeId, SubProductEnum.All.Value))) &&
                                      string.Equals(a.CompanyUserWorkflowState.Name, "approved"))
                          .ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    users = users.Where(a => a.Designation.Any(b => designationlist.Contains(b.DesignationId)))
                        .ToList();
                }

                foreach (var user in users)
                {
                    var subproductlist = user.ProductScopes.Where(a => a.ProductScopeId != null).Select(a => a.ProductScopeId)
                        .Distinct().ToList();
                    foreach (var subproductvalue in subproductlist)
                    {
                        TreatmentUsersSubProductName user1 = new TreatmentUsersSubProductName();

                        if (!string.Equals(subproductvalue, SubProductEnum.All.Value))
                        {
                            var fetchsubrpoductname = subproductidandnames.Where(a => a.SubproductId == subproductvalue)
                                .FirstOrDefault();
                            user1.AllocationId = user.Id;
                            user1.SubproductId = subproductvalue;
                            user1.SubproductName =
                                fetchsubrpoductname != null ? fetchsubrpoductname.SubproductName : "";
                            treatmentusernamelist.Add(user1);

                            var fetchuser = userWithAllConditions.Where(a => a.AllocationId == user1.AllocationId)
                                .FirstOrDefault();

                            UserWithAllCondition branchuser = new UserWithAllCondition();
                            branchuser.AllocationId = user1.AllocationId;
                            branchuser.SubProduct = user1.SubproductName;
                            userWithAllConditions.Add(branchuser);
                        }
                    }
                }

                foreach (var subproductvalue in subproduct)
                {
                    List<LoanAccount> subproductaccounts = new List<LoanAccount>();
                    subproductaccounts = AssignSameSubProductAsAccountUsers.Where(a => a.SubProduct == subproductvalue)
                        .ToList();

                    var users1 = treatmentusernamelist.Where(a => a.SubproductName == subproductvalue).ToArray();

                    int useridscount = users1.Count();

                    string[] usersids = users1.Select(a => a.AllocationId).ToArray();

                    if (usersids.Count() > 0)
                    {
                        accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                            subproductaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllConditions,
                            operatorvalue, filteredaccounts);
                    }
                }
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value))
            {
                List<string> responsibleids = new List<string>();
                List<AgencyUser> agencyusers = new List<AgencyUser>();

                if (operatorvalue == "and")
                {
                    List<string> tellecallerids = new List<string>();

                    tellecallerids = filteredaccounts.Select(a => a.TeleCallerId).ToList();

                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserProductScope()
                                  .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                  .Where(a => tellecallerids.Contains(a.Id) && a.AgencyUserWorkflowState != null &&
                                              a.ProductScopes.Any(
                                                  b => subproductids.Contains(b.ProductScopeId) ||
                                                       string.Equals(b.ProductScopeId, SubProductEnum.All.Value)) &&
                                              a.AgencyUserWorkflowState.Name.Contains("approved")
                                  ).ToListAsync();

                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                            .IncludeUserProductScope()
                                            .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                            .Where(a => a.AgencyUserWorkflowState != null &&
                                                 a.ProductScopes.Any(
                                                     b => subproductids.Contains(b.ProductScopeId) ||
                                                          string.Equals(b.ProductScopeId, SubProductEnum.All.Value)) &&
                                                 string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                            ).ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    agencyusers = agencyusers
                        .Where(a => a.Designation.Any(b => designationlist.Contains(b.DesignationId))).ToList();
                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                    .Where(a => agencyusers.Any(c => c.Id == a.ResponsibleId)
                                                                && string.Equals(a.AccountabilityTypeId, "tc")
                                                    ).ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var agencyuser in agencyusers)
                    {
                        var subproductlist = agencyuser.ProductScopes.Where(a => !string.IsNullOrEmpty(a.ProductScopeId) 
                                                && (string.Equals(a.ProductScopeId, SubProductEnum.All.Value) || a.ProductScopeId != null))
                                             .Select(a => a.ProductScopeId)
                                             .Distinct()
                                             .ToList();


                        foreach (var subproductvalue in subproductlist)
                        {
                            TreatmentUsersSubProductName user1 = new TreatmentUsersSubProductName();

                            if (!string.Equals(subproductvalue, SubProductEnum.All.Value))
                            {
                                var fetchsubrpoductname = subproductidandnames
                                    .Where(a => a.SubproductId == subproductvalue).FirstOrDefault();
                                user1.AllocationId = agencyuser.Id;
                                user1.SubproductId = subproductvalue;
                                user1.SubproductName =
                                    fetchsubrpoductname != null ? fetchsubrpoductname.SubproductName : "";
                                treatmentusernamelist.Add(user1);
                            }

                            var fetchuser = userWithAllConditions.Where(a => a.AllocationId == user1.AllocationId)
                                .FirstOrDefault();

                            //if (fetchuser != null)
                            //{
                            //    fetchuser.SubProduct = subproductvalue;
                            //    userWithAllConditions.Add(fetchuser);
                            //}
                            //else
                            //{
                            UserWithAllCondition branchuser = new UserWithAllCondition();
                            branchuser.AllocationId = user1.AllocationId;
                            branchuser.SubProduct = subproductvalue;
                            userWithAllConditions.Add(branchuser);

                            //}
                        }
                    }
                }

                foreach (var subproductvalue in subproduct)
                {
                    List<LoanAccount> subproductaccounts = new List<LoanAccount>();
                    subproductaccounts = AssignSameSubProductAsAccountUsers.Where(a => a.SubProduct == subproductvalue)
                        .ToList();

                    var users1 = treatmentusernamelist
                        .Where(a => a.SubproductName == subproductvalue || a.SubproductId == "All").ToArray();

                    int useridscount = users1.Count();

                    string[] usersids = users1.Select(a => a.AllocationId).ToArray();
                    accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                        subproductaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllConditions,
                        operatorvalue, filteredaccounts);
                }
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value))
            {
                List<string> responsibleids = new List<string>();
                List<AgencyUser> agencyusers = new List<AgencyUser>();
                if (operatorvalue == "and")
                {
                    List<string> fieldagentids = new List<string>();
                    fieldagentids = filteredaccounts.Select(a => a.CollectorId).ToList();
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                  .IncludeUserProductScope()
                                  .IncludeAgencyUserWorkflow()
                                  .IncludeAgencyUserDesignation()
                                  .Where(a => fieldagentids.Contains(a.Id)
                                      && a.AgencyUserWorkflowState != null
                                      && string.Equals(a.AgencyUserWorkflowState.Name, "approved")
                                      && a.ProductScopes.Any(b =>
                                          subproductids.Contains(b.ProductScopeId) ||
                                          string.Equals(b.ProductScopeId, SubProductEnum.All.Value)
                                      )
                                  ).ToListAsync();


                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserProductScope()
                                  .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                  .Where(a => a.AgencyUserWorkflowState != null &&
                                              a.ProductScopes.Any(
                                                  b => subproductids.Contains(b.ProductScopeId) ||
                                                       string.Equals(b.ProductScopeId, SubProductEnum.All.Value)) &&
                                              a.AgencyUserWorkflowState.Name.Contains("approved")
                                  ).ToListAsync();

                }

                if (designationlist.Count() > 0)
                {
                    agencyusers = agencyusers
                        .Where(a => a.Designation.Any(b => designationlist.Contains(b.DesignationId))).ToList();
                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                      .Where(a =>
                                                          agencyusers.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                                          a.AccountabilityTypeId.Contains("fos"))
                                                      .ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var agencyuser in agencyusers)
                    {
                        var subproductlist = agencyuser.ProductScopes
                                            .Where(a => !string.IsNullOrEmpty(a.ProductScopeId) && (string.Equals(a.ProductScopeId, SubProductEnum.All.Value) || a.ProductScopeId != null))
                                            .Select(a => a.ProductScopeId)
                                            .Distinct()
                                            .ToList();

                        foreach (var subproductvalue in subproductlist)
                        {
                            TreatmentUsersSubProductName user1 = new TreatmentUsersSubProductName();

                            if (!string.Equals(subproductvalue, SubProductEnum.All.Value))
                            {
                                var fetchsubrpoductname = subproductidandnames
                                    .Where(a => a.SubproductId == subproductvalue).FirstOrDefault();
                                user1.AllocationId = agencyuser.Id;
                                user1.SubproductId = subproductvalue;
                                user1.SubproductName =
                                    fetchsubrpoductname != null ? fetchsubrpoductname.SubproductName : "";
                                treatmentusernamelist.Add(user1);
                            }

                            var fetchuser = userWithAllConditions.Where(a => a.AllocationId == user1.AllocationId)
                                .FirstOrDefault();

                            //if (fetchuser != null)
                            //{
                            //    fetchuser.SubProduct = subproductvalue;
                            //    userWithAllConditions.Add(fetchuser);
                            //}
                            //else
                            //{
                            UserWithAllCondition branchuser = new UserWithAllCondition();
                            branchuser.AllocationId = user1.AllocationId;
                            branchuser.SubProduct = subproductvalue;
                            userWithAllConditions.Add(branchuser);

                            //}
                        }
                    }
                }

                foreach (var subproductvalue in subproduct)
                {
                    List<LoanAccount> subproductaccounts = new List<LoanAccount>();
                    subproductaccounts = AssignSameSubProductAsAccountUsers.Where(a => a.SubProduct == subproductvalue)
                        .ToList();

                    var users1 = treatmentusernamelist.Where(a => a.SubproductName == subproductvalue || string.Equals(a.SubproductId, SubProductEnum.All.Value)).ToArray();


                    int useridscount = users1.Count();

                    string[] usersids = users1.Select(a => a.AllocationId).ToArray();
                    accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                        subproductaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllConditions,
                        operatorvalue, filteredaccounts);
                }
            }

            List<LoanAccount> SameSubProductFilteredAccounts = new List<LoanAccount>();
            SameSubProductFilteredAccounts = accountToSave;
            return SameSubProductFilteredAccounts;
        }

        private async Task<List<LoanAccount>> AssignPersonaInSkillAsAccountUsers(ExecuteFragmentedTreatmentDataPacket packet,
            List<LoanAccount> accounts, string allocationType, int i,
            List<TreatmentLoanAccountDetails> accountToUpdateList, string ruleoperator, string tenantid, bool loadrule,
            List<string> designationList, AndConditionCheck andConditionCheck,
            List<UserWithAllCondition> userWithAllCondition, string operatorvalue = null,
            List<LoanAccount> filteredaccounts = null)
        {
            List<LoanAccount> AssignSameCustomerPersonaAsAccountUsers = new List<LoanAccount>();
            List<string> accountids = new List<string>();
            accountids = accounts.Select(b => b.Id).ToList();

            AssignSameCustomerPersonaAsAccountUsers = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                                .Where(a => accountids.Contains(a.Id))
                                                                .ToListAsync();

            List<string> customerpersonacodelist = AssignSameCustomerPersonaAsAccountUsers
                .Select(a => a.CustomerPersona.ToLower()).Distinct().ToList();
            List<TreatmentCustomerPersonaUsersName> treatmentcustomerpersonausernamelist =
                new List<TreatmentCustomerPersonaUsersName>();
            List<LoanAccount> accountToSave = new List<LoanAccount>();

            if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value) || string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
            {
                List<string> responsibleids = new List<string>();
                List<ApplicationUserSkill> applicationuserSkilllist = new List<ApplicationUserSkill>();

                List<CompanyUser> companyusers = new List<CompanyUser>();

                if (operatorvalue == "and")
                {
                    if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value))
                    {
                        List<string> bankstaffids = userWithAllCondition.Select(a => a.AllocationId).ToList();
                        companyusers = await _repoFactory.GetRepo().FindAll<CompanyUser>().CompanyUserCustomerPersona()
                            .IncludeCompanyUserWorkflow().IncludeDesignation()
                            .Where(a => bankstaffids.Contains(a.Id) &&
                                        string.Equals(a.CompanyUserWorkflowState.Name, "approved") &&
                                        a.Designation.Any(b => designationList.Contains(b.DesignationId))
                            ).ToListAsync();
                    }

                    if (string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
                    {
                        List<string> allocationownerids = filteredaccounts.Select(a => a.AllocationOwnerId).ToList();

                        companyusers = await _repoFactory.GetRepo().FindAll<CompanyUser>().CompanyUserCustomerPersona()
                            .IncludeCompanyUserWorkflow().IncludeDesignation()
                            .Where(a => allocationownerids.Contains(a.Id) &&
                                        string.Equals(a.CompanyUserWorkflowState.Name, "approved") &&
                                        a.Designation.Any(b => designationList.Contains(b.DesignationId))
                            ).ToListAsync();
                    }

                }
                else
                {
                    companyusers = await _repoFactory.GetRepo().FindAll<CompanyUser>().CompanyUserCustomerPersona()
                                    .IncludeCompanyUserWorkflow().IncludeDesignation()
                                    .Where(a => string.Equals(a.CompanyUserWorkflowState.Name, "approved") &&
                                                a.Designation.Any(b => designationList.Contains(b.DesignationId))
                                    ).ToListAsync();
                }

                if (companyusers.Count() > 0)
                {
                    FetchUserListForAllocation(treatmentcustomerpersonausernamelist, applicationuserSkilllist,
                        companyusers, userWithAllCondition);
                }

                await CustomerPersonaAllocateToUserAsync(packet, allocationType, accountToUpdateList, tenantid,
                    AssignSameCustomerPersonaAsAccountUsers, customerpersonacodelist,
                    treatmentcustomerpersonausernamelist, accountToSave, loadrule, operatorvalue, filteredaccounts,
                    andConditionCheck, userWithAllCondition);
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value))
            {
                List<string> responsibleids = new List<string>();

                List<ApplicationUserSkill> applicationuserSkilllist = new List<ApplicationUserSkill>();
                List<AgencyUser> agencyusers = new List<AgencyUser>();

                if (operatorvalue == "and")
                {
                    List<string> tellecallerids = new List<string>();
                    tellecallerids = filteredaccounts.Select(a => a.TeleCallerId).ToList();
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserCustomerPersona()
                                 .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                 .Where(a => tellecallerids.Contains(a.Id) &&
                                             string.Equals(a.AgencyUserWorkflowState.Name, "approved") &&
                                             a.Designation.Any(b => designationList.Contains(b.DesignationId))
                                 ).ToListAsync();

                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserCustomerPersona()
                                  .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                  .Where(a => string.Equals(a.AgencyUserWorkflowState.Name, "approved") &&
                                              a.Designation.Any(b => designationList.Contains(b.DesignationId))
                                  ).ToListAsync();

                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>().Where(a =>
                                             agencyusers.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                             a.AccountabilityTypeId.Contains("tc")
                                         ).ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var ausers in agencyusers)
                    {
                        if (ausers.userCustomerPersona != null && ausers.userCustomerPersona.Count() > 0)
                        {
                            var customerpersonalist = ausers.userCustomerPersona.ToList();

                            if (customerpersonalist != null)
                            {
                                foreach (var split in customerpersonalist)
                                {
                                    ApplicationUserSkill companyuserskill = new ApplicationUserSkill();
                                    companyuserskill.Id = ausers.Id;
                                    companyuserskill.CustomerPersona = split.Name;
                                    applicationuserSkilllist.Add(companyuserskill);
                                }
                            }
                        }
                    }

                    foreach (var agencyuser in applicationuserSkilllist)
                    {
                        TreatmentCustomerPersonaUsersName user1 = new TreatmentCustomerPersonaUsersName();
                        user1.AllocationId = agencyuser.Id;
                        user1.CustomerPersona = agencyuser.CustomerPersona;
                        treatmentcustomerpersonausernamelist.Add(user1);
                    }
                }

                await CustomerPersonaAllocateToUserAsync(packet, allocationType, accountToUpdateList, tenantid,
                    AssignSameCustomerPersonaAsAccountUsers, customerpersonacodelist,
                    treatmentcustomerpersonausernamelist, accountToSave, loadrule, operatorvalue, filteredaccounts,
                    andConditionCheck, userWithAllCondition);
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value))
            {
                List<string> responsibleids = new List<string>();
                List<ApplicationUserSkill> applicationuserSkilllist = new List<ApplicationUserSkill>();

                List<AgencyUser> agencyusers = new List<AgencyUser>();
                if (operatorvalue == "and")
                {
                    List<string> fieldagentids = new List<string>();
                    fieldagentids = filteredaccounts.Select(a => a.CollectorId).ToList();
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserCustomerPersona()
                                  .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                  .Where(a => fieldagentids.Contains(a.Id) &&
                                              string.Equals(a.AgencyUserWorkflowState.Name, "approved") &&
                                              a.Designation.Any(b => designationList.Contains(b.DesignationId)))
                                  .ToListAsync();

                }
                else
                {
                    agencyusers = await _repoFactory.GetRepo().FindAll<AgencyUser>().IncludeUserCustomerPersona()
                                 .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                 .Where(a => string.Equals(a.AgencyUserWorkflowState.Name, "approved") &&
                                             a.Designation.Any(b => designationList.Contains(b.DesignationId)))
                                 .ToListAsync();

                }

                if (agencyusers.Count() > 0)
                {
                    var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                          .Where(a => agencyusers.Select(c => c.Id).Contains(a.ResponsibleId) &&
                                                      string.Equals(a.AccountabilityTypeId, "fos"))
                                          .ToListAsync();


                    responsibleids = accountabilities.Select(a => a.ResponsibleId).ToList();

                    agencyusers = agencyusers.Where(a => responsibleids.Contains(a.Id)).ToList();

                    foreach (var ausers in agencyusers)
                    {
                        if (ausers.userCustomerPersona != null && ausers.userCustomerPersona.Count() > 0)
                        {
                            var customerpersonalist = ausers.userCustomerPersona.ToList();

                            if (customerpersonalist != null)
                            {
                                foreach (var split in customerpersonalist)
                                {
                                    ApplicationUserSkill companyuserskill = new ApplicationUserSkill();
                                    companyuserskill.Id = ausers.Id;
                                    companyuserskill.CustomerPersona = split.Name;
                                    applicationuserSkilllist.Add(companyuserskill);
                                }
                            }
                        }
                    }

                    foreach (var agencyuser in applicationuserSkilllist)
                    {
                        TreatmentCustomerPersonaUsersName user1 = new TreatmentCustomerPersonaUsersName();
                        user1.AllocationId = agencyuser.Id;
                        user1.CustomerPersona = agencyuser.CustomerPersona;
                        treatmentcustomerpersonausernamelist.Add(user1);
                    }
                }

                await CustomerPersonaAllocateToUserAsync(packet, allocationType, accountToUpdateList, tenantid,
                    AssignSameCustomerPersonaAsAccountUsers, customerpersonacodelist,
                    treatmentcustomerpersonausernamelist, accountToSave, loadrule, operatorvalue, filteredaccounts,
                    andConditionCheck, userWithAllCondition);
            }

            List<LoanAccount> PersonaFilteredAccounts = new List<LoanAccount>();
            PersonaFilteredAccounts = accountToSave;
            return PersonaFilteredAccounts;
        }

        private async Task CustomerPersonaAllocateToUserAsync(ExecuteFragmentedTreatmentDataPacket packet, string allocationType,
            List<TreatmentLoanAccountDetails> accountToUpdateList, string tenantid,
            List<LoanAccount> AssignSameCustomerPersonaAsAccountUsers, List<string> customerpersonalist,
            List<TreatmentCustomerPersonaUsersName> treatmentcustomerpersonanamelist, List<LoanAccount> accountToSave,
            bool loadrule, string operatorvalue, List<LoanAccount> filteredaccounts,
            AndConditionCheck andConditionCheck, List<UserWithAllCondition> userWithAllConditions)
        {
            foreach (var cp in customerpersonalist)
            {
                List<LoanAccount> cpaccounts = new List<LoanAccount>();
                cpaccounts = AssignSameCustomerPersonaAsAccountUsers
                               .Where(a => string.Equals(a.CustomerPersona, cp))
                               .ToList();

                var users = treatmentcustomerpersonanamelist
                    .Where(a => string.Equals(a.CustomerPersona, cp))
                    .ToArray();


                int useridscount = users.Count();
                string[] usersids = users.Select(a => a.AllocationId).ToArray();
                if (usersids.Count() > 0)
                {
                    accountToSave = await RoundRobinAllocationAsync(packet.Cmd.Dto.TreatmentId, allocationType, accountToSave,
                        cpaccounts, usersids, loadrule, tenantid, andConditionCheck, userWithAllConditions,
                        operatorvalue, filteredaccounts);
                }
            }
        }

        private static void FetchUserListForAllocation(
            List<TreatmentCustomerPersonaUsersName> treatmentcustomerpersonausernamelist,
            List<ApplicationUserSkill> applicationuserSkilllist, List<CompanyUser> companyusers,
            List<UserWithAllCondition> userWithAllCondition)
        {
            foreach (var cusers in companyusers)
            {
                if (cusers.userCustomerPersona != null && cusers.userCustomerPersona.Count() > 0)
                {
                    var areacodes = cusers.userCustomerPersona.ToList();

                    if (areacodes != null)
                    {
                        foreach (var split in areacodes)
                        {
                            ApplicationUserSkill companyuserskill = new ApplicationUserSkill();
                            companyuserskill.Id = cusers.Id;
                            companyuserskill.CustomerPersona = split.Name;
                            applicationuserSkilllist.Add(companyuserskill);
                        }
                    }
                }
            }

            foreach (var companyuser in applicationuserSkilllist)
            {
                TreatmentCustomerPersonaUsersName user1 = new TreatmentCustomerPersonaUsersName();
                user1.AllocationId = companyuser.Id;
                user1.CustomerPersona = companyuser.CustomerPersona;
                treatmentcustomerpersonausernamelist.Add(user1);
                UserWithAllCondition branchuser = new UserWithAllCondition();
                branchuser.AllocationId = user1.AllocationId;
                branchuser.CustomerPersona = companyuser.CustomerPersona;
                userWithAllCondition.Add(branchuser);
            }
        }
    }
}