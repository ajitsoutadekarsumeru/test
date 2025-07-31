using ENTiger.ENCollect.TreatmentModule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Data;
using Serilog.Core;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    public class TreatmentCommonFunctions : ITreatmentCommonFunctions
    {
        protected string EventCondition = "";
        protected readonly ILogger<TreatmentCommonFunctions> _logger;// = FlexContainer.ServiceProvider.GetService<ILogger<TreatmentCommonFunctions>>();
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Treatment? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly DatabaseSettings _databaseSettings;
        private string hostName = string.Empty;
        private string sqlConnectionString = string.Empty;
        private string dbType = string.Empty;
        //public TreatmentCommonFunctions(ExecuteTreatmentDto executeTreatmentDto)
        //{
        //    _repoFactory = new IRepoProvider();
        //}

        public TreatmentCommonFunctions(ILogger<TreatmentCommonFunctions> logger, IFlexHost flexHost, IRepoFactory repoFactory, IOptions<DatabaseSettings> databaseSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _repoFactory = repoFactory;
            _databaseSettings = databaseSettings.Value;
        }

        public LoanAccount AssignToAccount(string allocationId, LoanAccount acc, string TreatmentId, string allocationType)
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
                acc.CollectorId = allocationId.ToString();
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
            return acc;
        }


        public async Task ConstructDatatable(ExecuteFragmentedTreatmentDataPacket packet, List<LoanAccount> accountToSave, ExecuteTreatmentDto executeTreatmentDto)
        {
            _repoFactory.Init(executeTreatmentDto);
            if (accountToSave.Count > 0)
            {
                dbType = _databaseSettings.DBType;
                string workrequestid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
                if (string.Equals(dbType, DBTypeEnum.MySQL.Value, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var acc in accountToSave)
                    {
                        acc.SetModified();
                        acc.SetLastModifiedDate(DateTime.Now);
                        _repoFactory.GetRepo().InsertOrUpdate(acc);
                        await _repoFactory.GetRepo().SaveAsync();
                    }
                    packet.Cmd.Dto.WorkRequestId = workrequestid;
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

                    foreach (var acc in accountToSave)
                    {
                        dt.Rows.Add(SequentialGuid.NewGuidString(), acc.AGREEMENTID, acc.AllocationOwnerId, acc.TeleCallingAgencyId, acc.AgencyId, acc.TeleCallerId, acc.CollectorId, packet.Cmd.Dto.TreatmentId, workrequestid, 0, DateTime.Now, DateTime.Now);
                    }

                    hostName = packet.Cmd.Dto.GetAppContext().HostName;
                    //fetch connection string
                    IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();
                    var sqlConnectionString = await _repoTenantFactory.FindAll<FlexTenantBridge>()
                                                            .Where(x => x.Id == _flexAppContext.TenantId)
                                                            .Select(x => x.DefaultWriteDbConnectionString)
                                                            .FirstOrDefaultAsync();

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

        public async Task<List<ElasticSearchSimulateLoanAccountDto>> FetchAccountsForAllocationAsync(List<ElasticSearchSimulateLoanAccountDto> loanAccounts, string allocationType, string TenantId, string TreatmentId, ExecuteTreatmentDto executeTreatmentDto)
        {
            try
            {
                _repoFactory.Init(executeTreatmentDto);
                List<string> agreementids = loanAccounts.Select(a => a.agreementid).ToList();
                List<string?> userids = new List<string?>();
                List<string?> fullaccountList = new List<string?>();
                List<string?> exceptaccountList = new List<string?>();
                List<ElasticSearchSimulateLoanAccountDto> finalaccountlist = new List<ElasticSearchSimulateLoanAccountDto>();

                if (string.Equals(allocationType, AllocationTypeEnum.TeleCallingAgency.Value, StringComparison.OrdinalIgnoreCase))
                {
                    var accountwithAgencyId = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                    .Where(a => agreementids.Contains(a.AGREEMENTID))
                                                    .Select(a => new { TeleCallingagencyID = a.TeleCallingAgencyId, ID = a.Id, AgreementID = a.AGREEMENTID }).ToListAsync();
                    if (accountwithAgencyId.Count() > 0)
                    {
                        userids = accountwithAgencyId.Select(a => a.TeleCallingagencyID).Distinct().ToList();
                        fullaccountList = accountwithAgencyId.Select(a => a.AgreementID).Distinct().ToList();
                    }

                    var approvedagencies = await _repoFactory.GetRepo().FindAll<Agency>()
                                                    .IncludeAgencyWorkflow()
                                                    .Where(a => userids.Contains(a.Id)
                                                            && string.Equals(a.AgencyWorkflowState.Name, "approved", StringComparison.OrdinalIgnoreCase)
                                                    ).ToListAsync();

                    List<string> agencyids = new List<string>();
                    agencyids = approvedagencies.Select(a => a.Id).ToList();
                    exceptaccountList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                    .Where(a => agreementids.Contains(a.AGREEMENTID)
                                                                && agencyids.Contains(a.TeleCallingAgencyId)
                                                                && a.TreatmentId == TreatmentId).Select(a => a.AGREEMENTID).ToListAsync();
                    string[] exceptarrays = exceptaccountList.ToArray();
                    string[] accountsarrays = fullaccountList.ToArray();
                    IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);
                    finalaccountlist = loanAccounts.Where(a => filteredlist.Contains(a.agreementid)).ToList();
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgency.Value, StringComparison.OrdinalIgnoreCase))
                {
                    var accountwithAgencyId = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                    .Where(a => agreementids.Contains(a.AGREEMENTID))
                                                    .Select(a => new { FieldagencyID = a.AgencyId, ID = a.Id, AgreementID = a.AGREEMENTID })
                                                    .ToListAsync();
                    if (accountwithAgencyId.Count() > 0)
                    {
                        userids = accountwithAgencyId.Select(a => a.FieldagencyID).Distinct().ToList();
                        fullaccountList = accountwithAgencyId.Select(a => a.AgreementID).Distinct().ToList();
                    }
                    var approvedagencies = await _repoFactory.GetRepo().FindAll<Agency>()
                                                    .IncludeAgencyWorkflow()
                                                    .Where(a => userids.Contains(a.Id) &&
                                                        a.AgencyWorkflowState.Name.Contains("approved", StringComparison.OrdinalIgnoreCase))
                                                    .ToListAsync();

                    List<string> agencyids = new List<string>();
                    agencyids = approvedagencies.Select(a => a.Id).ToList();
                    exceptaccountList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                    .Where(a => agreementids.Contains(a.AGREEMENTID) &&
                                                            agencyids.Contains(a.AgencyId) && a.TreatmentId == TreatmentId)
                                                    .Select(a => a.AGREEMENTID).ToListAsync();
                    string[] exceptarrays = exceptaccountList.ToArray();
                    string[] accountsarrays = fullaccountList.ToArray();
                    IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);
                    finalaccountlist = loanAccounts.Where(a => filteredlist.Contains(a.agreementid)).ToList();
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value, StringComparison.OrdinalIgnoreCase))
                {
                    var accountwithCollectorId = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                    .Where(a => agreementids.Contains(a.AGREEMENTID))
                                                    .Select(a => new { CollectorID = a.CollectorId, ID = a.Id, AgreementID = a.AGREEMENTID }).ToListAsync();
                    if (accountwithCollectorId.Count() > 0)
                    {
                        userids = accountwithCollectorId.Select(a => a.CollectorID).Distinct().ToList();
                        fullaccountList = accountwithCollectorId.Select(a => a.AgreementID).Distinct().ToList();
                        _logger.LogInformation("FetchAccountsForAllocation accountwithCollectorId userids " + userids.Count());
                        _logger.LogInformation("FetchAccountsForAllocation accountwithCollectorId fullaccountList " + fullaccountList.Count());
                    }
                    var approvedstaffs = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                            .IncludeCompanyUserWorkflow()
                                            .Where(a => userids.Contains(a.Id) &&
                                                    a.CompanyUserWorkflowState.Name.Contains("approved", StringComparison.OrdinalIgnoreCase))
                                            .ToListAsync();

                    List<string> staffids = new List<string>();
                    staffids = approvedstaffs.Select(a => a.Id).ToList();
                    exceptaccountList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .Where(a => agreementids.Contains(a.AGREEMENTID) &&
                                                                staffids.Contains(a.CollectorId) && a.TreatmentId == TreatmentId)
                                                .Select(a => a.AGREEMENTID).ToListAsync();
                    _logger.LogInformation("FetchAccountsForAllocation accountwithCollectorId exceptaccountList " + exceptaccountList.Count());
                    string[] exceptarrays = exceptaccountList.ToArray();
                    string[] accountsarrays = fullaccountList.ToArray();
                    IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);
                    _logger.LogInformation("FetchAccountsForAllocation accountwithCollectorId filteredlist " + filteredlist.Count());
                    finalaccountlist = loanAccounts.Where(a => filteredlist.Contains(a.agreementid)).ToList();
                    _logger.LogInformation("FetchAccountsForAllocation accountwithCollectorId finalaccountlist " + finalaccountlist.Count());
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value, StringComparison.OrdinalIgnoreCase))
                {
                    var accountwithAllocationownerId = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                        .Where(a => agreementids.Contains(a.AGREEMENTID))
                                        .Select(a => new { AllocationOwnerID = a.AllocationOwnerId, ID = a.Id, AgreementID = a.AGREEMENTID }).ToListAsync();

                    if (accountwithAllocationownerId.Count() > 0)
                    {
                        userids = accountwithAllocationownerId.Select(a => a.AllocationOwnerID).Distinct().ToList();
                        fullaccountList = accountwithAllocationownerId.Select(a => a.AgreementID).Distinct().ToList();
                    }
                    var approvedstaffs = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                                .IncludeCompanyUserWorkflow()
                                                .Where(a => userids.Contains(a.Id) &&
                                                    a.CompanyUserWorkflowState.Name.Contains("approved"))
                                                .ToListAsync();

                    List<string> staffids = new List<string>();
                    staffids = approvedstaffs.Select(a => a.Id).ToList();

                    exceptaccountList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .Where(a => agreementids.Contains(a.AGREEMENTID) &&
                                                            staffids.Contains(a.AllocationOwnerId) &&
                                                            a.TreatmentId == TreatmentId)
                                                .Select(a => a.AGREEMENTID)
                                                .ToListAsync();

                    string[] exceptarrays = exceptaccountList.ToArray();
                    string[] accountsarrays = fullaccountList.ToArray();
                    IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);

                    finalaccountlist = loanAccounts.Where(a => filteredlist.Contains(a.agreementid)).ToList();
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value, StringComparison.OrdinalIgnoreCase))
                {
                    var accountwithTelecallerId = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                        .Where(a => agreementids.Contains(a.AGREEMENTID))
                                                        .Select(a => new { TeleCallerID = a.TeleCallerId, ID = a.Id, AgreementID = a.AGREEMENTID }).ToListAsync();

                    if (accountwithTelecallerId.Count() > 0)
                    {
                        userids = accountwithTelecallerId.Select(a => a.TeleCallerID).Distinct().ToList();
                        fullaccountList = accountwithTelecallerId.Select(a => a.AgreementID).Distinct().ToList();
                    }

                    var approvedtelecallers = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                                    .IncludeAgencyUserWorkflow()
                                                    .Where(a => userids.Contains(a.Id) && a.AgencyUserWorkflowState.Name.Contains("approved"))
                                                    .ToListAsync();


                    List<string> telecallerids = new List<string>();
                    telecallerids = approvedtelecallers.Select(a => a.Id).ToList();

                    exceptaccountList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                .Where(a => agreementids.Contains(a.AGREEMENTID) &&
                                                        telecallerids.Contains(a.TeleCallerId) && a.TreatmentId == TreatmentId)
                                                .Select(a => a.AGREEMENTID).ToListAsync();

                    string[] exceptarrays = exceptaccountList.ToArray();
                    string[] accountsarrays = fullaccountList.ToArray();
                    IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);

                    finalaccountlist = loanAccounts.Where(a => filteredlist.Contains(a.agreementid)).ToList();
                }
                else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value, StringComparison.OrdinalIgnoreCase))
                {
                    var accountwithCollectorId = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                                        .Where(a => agreementids.Contains(a.AGREEMENTID))
                                                        .Select(a => new { CollectorID = a.CollectorId, ID = a.Id, AgreementID = a.AGREEMENTID }).ToListAsync();

                    if (accountwithCollectorId.Count() > 0)
                    {
                        userids = accountwithCollectorId.Select(a => a.CollectorID).Distinct().ToList();
                        fullaccountList = accountwithCollectorId.Select(a => a.AgreementID).Distinct().ToList();
                    }

                    var approvedfieldagents = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                                    .IncludeAgencyUserWorkflow()
                                                    .Where(a => userids.Contains(a.Id) && a.AgencyUserWorkflowState.Name.Contains("approved")).ToListAsync();

                    List<string> fieldagentids = new List<string>();
                    fieldagentids = approvedfieldagents.Select(a => a.Id).ToList();

                    exceptaccountList = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                            .Where(a => agreementids.Contains(a.AGREEMENTID) &&
                                                    fieldagentids.Contains(a.CollectorId) && a.TreatmentId == TreatmentId)
                                            .Select(a => a.AGREEMENTID).ToListAsync();

                    string[] exceptarrays = exceptaccountList.ToArray();
                    string[] accountsarrays = fullaccountList.ToArray();
                    IEnumerable<string> filteredlist = accountsarrays.Except(exceptarrays);

                    finalaccountlist = loanAccounts.Where(a => filteredlist.Contains(a.agreementid)).ToList();
                }

                return finalaccountlist;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message + " > " + ex.StackTrace + " > " + ex.ToString();
                if (ex.InnerException != null)
                    errorMsg += " >> " + ex.InnerException.Message + " > " + ex.InnerException.StackTrace + " > " + ex.InnerException.ToString();

                _logger.LogInformation("Exception in TreatmentCommonFunctions FetchAccountsForAllocation " + errorMsg);
                throw ex;
            }
        }

        public async Task<List<string>> FetchAccountsBasedOnQualifyingConditionDigitalCommunicationAsync(List<ElasticSearchSimulateLoanAccountDto> loanAccounts, string allocationType, string TenantId, string DeliveryStatus, string TreatmentId, string SubTreatmentId, string SubTreatmentIdLatest, string newTreatmentHistoryId)
        {
            List<string> agreementids = new List<string>();
            List<string> packetaccids = new List<string>();
            try
            {
                _logger.LogInformation("Entered into FetchAccountsBasedOnQualifyingConditionDigitalCommunication ");
                List<ElasticSearchSimulateLoanAccountDto> finalaccountlist = new List<ElasticSearchSimulateLoanAccountDto>();

                finalaccountlist = loanAccounts;
                _logger.LogInformation("SubTreatmentIdLatest " + SubTreatmentIdLatest);
                _logger.LogInformation("SubTreatmentId " + SubTreatmentId);
                _logger.LogInformation("DeliveryStatus " + DeliveryStatus);
                packetaccids = finalaccountlist.Select(a => a.id).ToList();

                var communicationhistorydetails = await _repoFactory.GetRepo().FindAll<TreatmentOnCommunicationHistoryDetails>()
                                                            .Where(a => packetaccids.Contains(a.LoanAccountId) &&
                                                                    a.SubTreatmentId == SubTreatmentId &&
                                                                    a.WADeliveredStatus == DeliveryStatus)
                                                            .OrderByDescending(a => a.LastModifiedDate).ToListAsync();

                var communicationhistorydetailsToday = await _repoFactory.GetRepo().FindAll<TreatmentOnCommunicationHistoryDetails>()
                                                            .Where(a => packetaccids.Contains(a.LoanAccountId) && (a.SubTreatmentId == SubTreatmentIdLatest))
                                                            .OrderByDescending(a => a.LastModifiedDate).ToListAsync();

                var communicationhistorydetailstotal = await _repoFactory.GetRepo().FindAll<TreatmentOnCommunicationHistoryDetails>()
                                                            .Where(a => packetaccids.Contains(a.LoanAccountId))
                                                            .OrderByDescending(a => a.LastModifiedDate).ToListAsync();

                _logger.LogInformation("communicationhistorydetails count " + communicationhistorydetails.Count());

                DateTime currentDate = DateTime.Now.Date;
                DateTime startDate = currentDate;
                DateTime endDate = startDate.AddDays(1);

                DateOnly currentDateOnly = DateOnly.FromDateTime(currentDate);
                if (communicationhistorydetails != null)
                    {
                        DateTime createdDate = currentDate;
                        bool historydetailsgreaterthanzero = false;
                        bool historydetailsequalstozero = false;
                        if (communicationhistorydetailsToday.Count() > 0)
                        {
                            historydetailsgreaterthanzero = true;
                            createdDate = communicationhistorydetailsToday.OrderByDescending(a => a.DeliveryDate).Select(a => a.DeliveryDate.Value).FirstOrDefault();
                        }
                        else
                        {
                            historydetailsequalstozero = true;
                            createdDate = communicationhistorydetails.OrderByDescending(a => a.DeliveryDate).Select(a => a.DeliveryDate.Value).FirstOrDefault();
                        }

                        _logger.LogInformation("createdDate " + createdDate);
                        _logger.LogInformation("createdDate TotalHourd " + currentDate.Subtract(createdDate.Date).TotalHours);
                        
                        if ((historydetailsgreaterthanzero = true && currentDate.Subtract(createdDate.Date).TotalHours >= 24) 
                            || ((historydetailsequalstozero = true && currentDate.Subtract(createdDate.Date).TotalHours >= 0)))
                        {
                            List<string> loanaccountids = new List<string>();
                            List<string> packetaccounts = new List<string>();
                            loanaccountids = communicationhistorydetails.Select(a => a.LoanAccountId).Distinct().ToList();
                            packetaccounts = finalaccountlist.Select(a => a.id).ToList();
                            _logger.LogInformation(" yesterday loanaccountids  count " + loanaccountids.Count());

                        if (communicationhistorydetailsToday != null)
                        {
                            var subtreatment = await _repoFactory.GetRepo().FindAll<SubTreatment>().Where(a => a.Id == SubTreatmentIdLatest).FirstOrDefaultAsync();
                            _logger.LogInformation(" Today communicationhistorydetailsToday  count " + communicationhistorydetailsToday.Count());
                            if (communicationhistorydetailsToday.Count != 0)
                            {
                                _logger.LogInformation(" communicationhistorydetailsToday count > 0 " + communicationhistorydetailsToday.Count());

                                if (subtreatment.StartDay == subtreatment.EndDay)
                                {
                                    _logger.LogInformation(" subtreatment.StartDay == subtreatment.EndDay " + subtreatment.StartDay + " " + subtreatment.EndDay);
                                    var lstids = communicationhistorydetailsToday.Where(a => a.SubTreatmentId == SubTreatmentIdLatest).ToList();

                                    var lstaccids = communicationhistorydetailsToday.Where(a => a.DeliveryDate_Only.HasValue 
                                            && a.DeliveryDate_Only == currentDateOnly && packetaccounts.Contains(a.LoanAccountId)).ToList();

                                        _logger.LogInformation("lstids ", lstids.Count());
                                        _logger.LogInformation("loanaccountids ", loanaccountids.Count());
                                        lstids.ForEach(a =>
                                        {
                                            var firstcommunication = communicationhistorydetailstotal.Where(b => b.LoanAccountId == a.LoanAccountId)
                                                    .Select(b => b.DeliveryDate_Only.Value).Distinct().Count();
                                            double startday = Convert.ToDouble(subtreatment.StartDay);
                                            //if (firstcommunication != null)
                                            //{

                                            /*
                                            //double datediff = (Convert.ToDateTime(firstcommunication.DeliveryDate) - DateTime.Now.Date).Days;
                                            double datediff = Math.Floor((DateTime.Now - Convert.ToDateTime(firstcommunication.DeliveryDate)).TotalDays)+1;
                                            //double datediff = DateTime.Now.Date.Subtract(Convert.ToDateTime(firstcommunication.DeliveryDate)).TotalDays;


                                            if(firstcommunication.DeliveryDate <= DateTime.Now)
                                            {

                                                    if(firstcommunication.DeliveryDate.Value.DayOfWeek == DayOfWeek.Sunday)
                                                    {
                                                        datediff = datediff - 1;
                                                    }

                                            }
                                            */
                                            if (firstcommunication + 1 != startday)
                                            {
                                                loanaccountids.Remove(a.LoanAccountId);
                                            }
                                        });
                                        _logger.LogInformation("loanaccountids after ", loanaccountids.Count());

                                    _logger.LogInformation("lstaccids before ", loanaccountids.Count());
                                    lstaccids.ForEach(a => { loanaccountids.Remove(a.LoanAccountId); });
                                    _logger.LogInformation("lstaccids after ", loanaccountids.Count());
                                }
                                else if (Convert.ToInt32(subtreatment.StartDay) < Convert.ToInt32(subtreatment.EndDay))
                                {
                                    _logger.LogInformation(" subtreatment.StartDay < subtreatment.EndDay " + subtreatment.StartDay + " " + subtreatment.EndDay);

                                    int numberofdaysTrigger = (Convert.ToInt32(subtreatment.EndDay) - Convert.ToInt32(subtreatment.StartDay)) + 1;
                                    _logger.LogInformation(" numberofdaysTrigger " + numberofdaysTrigger);
                                    var lstids = communicationhistorydetailsToday.Where(a => a.SubTreatmentId == SubTreatmentIdLatest).ToList();


                                        lstids.ForEach(a =>
                                        {
                                            if (lstids.Where(b => b.LoanAccountId == a.LoanAccountId).ToList().Count() == numberofdaysTrigger)
                                            {
                                                loanaccountids.Remove(a.LoanAccountId);
                                            }
                                        });

                                        var lstaccids = communicationhistorydetailsToday.Where(a => a.DeliveryDate_Only.HasValue 
                                                && a.DeliveryDate_Only == currentDateOnly && packetaccounts.Contains(a.LoanAccountId)).ToList();
                                        _logger.LogInformation("lstaccids before ", loanaccountids.Count());
                                        lstaccids.ForEach(a => { loanaccountids.Remove(a.LoanAccountId); });
                                        _logger.LogInformation("lstaccids after ", loanaccountids.Count());
                                    }
                                }
                                else
                                {
                                    _logger.LogInformation(" ELSE NEW DIGITAL ");
                                var subtreatment1 = await _repoFactory.GetRepo().FindAll<SubTreatment>().Where(a => a.Id == SubTreatmentIdLatest).FirstOrDefaultAsync();
                                    if (subtreatment1.StartDay == subtreatment1.EndDay)
                                    {
                                        _logger.LogInformation(" subtreatment.StartDay == subtreatment.EndDay else condition " + subtreatment1.StartDay + " " + subtreatment1.EndDay);
                                        _logger.LogInformation(" SubTreatmentIdLatest Id " + SubTreatmentIdLatest);

                                    var lstids = communicationhistorydetailsToday.Where(a => a.SubTreatmentId == SubTreatmentIdLatest).ToList();
                                    _logger.LogInformation(" lstids " + lstids.Count());
                                    _logger.LogInformation("loanaccountids ", loanaccountids.Count());
                                    lstids.ForEach(a =>
                                    {
                                        var firstcommunication = communicationhistorydetailstotal.Where(b => b.LoanAccountId == a.LoanAccountId)
                                                .Select(b => b.DeliveryDate_Only.Value).Distinct().Count();
                                        double startday = Convert.ToDouble(subtreatment.StartDay);

                                        if (firstcommunication + 1 != startday)
                                        {
                                            loanaccountids.Remove(a.LoanAccountId);
                                        }
                                    });

                                    _logger.LogInformation(" loanaccountids " + loanaccountids.Count());

                                        var lstaccids = communicationhistorydetailsToday.Where(a => a.DeliveryDate_Only.HasValue 
                                                && a.DeliveryDate_Only == currentDateOnly && packetaccounts.Contains(a.LoanAccountId)).ToList();
                                        _logger.LogInformation("lstaccids before ", loanaccountids.Count());
                                        lstaccids.ForEach(a => { loanaccountids.Remove(a.LoanAccountId); });
                                        _logger.LogInformation("lstaccids after ", loanaccountids.Count());
                                    }
                                    else if (Convert.ToInt32(subtreatment1.StartDay) < Convert.ToInt32(subtreatment1.EndDay))
                                    {
                                        int numberofdaysTrigger = (Convert.ToInt32(subtreatment1.EndDay) - Convert.ToInt32(subtreatment1.StartDay)) + 1;
                                        _logger.LogInformation(" numberofdaysTrigger " + numberofdaysTrigger);
                                        _logger.LogInformation(" SubTreatmentIdLatest ID " + SubTreatmentIdLatest);
                                        var lstids = communicationhistorydetailsToday.Where(a => a.SubTreatmentId == SubTreatmentIdLatest).ToList();
                                        _logger.LogInformation(" lstids " + lstids.Count());
                                        _logger.LogInformation("loanaccountids ", loanaccountids.Count());
                                        lstids.ForEach(a =>
                                        {

                                            if (lstids.Where(b => b.LoanAccountId == a.LoanAccountId).ToList().Count() == numberofdaysTrigger)
                                            {

                                                loanaccountids.Remove(a.LoanAccountId);
                                            }

                                        });

                                        var lstaccids = communicationhistorydetailsToday.Where(a => a.DeliveryDate_Only.HasValue 
                                            && a.DeliveryDate_Only == currentDateOnly
                                            && packetaccounts.Contains(a.LoanAccountId)).ToList();
                                        _logger.LogInformation("lstaccids before ", loanaccountids.Count());
                                        lstaccids.ForEach(a => { loanaccountids.Remove(a.LoanAccountId); });
                                        _logger.LogInformation("lstaccids after ", loanaccountids.Count());
                                    }
                                }
                            }

                        finalaccountlist = finalaccountlist.Where(a => loanaccountids.Contains(a.id)).ToList();

                        agreementids = finalaccountlist.Select(b => b.agreementid).ToList();
                        _logger.LogInformation("agreementids count " + agreementids.Count());
                    }
                }

                //}
                return agreementids;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message + " > " + ex.StackTrace + " > " + ex.ToString();
                if (ex.InnerException != null)
                    errorMsg += " >> " + ex.InnerException.Message + " > " + ex.InnerException.StackTrace + " > " + ex.InnerException.ToString();

                _logger.LogInformation("Exception in TreatmentCommonFunctions FetchAccountsForAllocation " + errorMsg);
                return agreementids;
                //throw ex;
            }
        }

        public async Task<List<string>> FetchAccountsBasedOnMultipleQualifyingCondition(List<ElasticSearchSimulateLoanAccountDto> loanAccounts, string allocationType, string TenantId, List<string> DeliveryStatus, string TreatmentId, string SubTreatmentId, string newTreatmentHistoryId, ExecuteTreatmentDto executeTreatmentDto)
        {
            List<string> agreementids = new List<string>();
            _repoFactory.Init(executeTreatmentDto);
            try
            {
                _logger.LogInformation("Entered into FetchAccountsBasedOnMultipleQualifyingCondition ");
                List<ElasticSearchSimulateLoanAccountDto> finalaccountlist = new List<ElasticSearchSimulateLoanAccountDto>();

                finalaccountlist = loanAccounts;

                TreatmentHistory? treatmenthistory = await _repoFactory.GetRepo().FindAll<TreatmentHistory>()
                                        .Where(a => a.Id != newTreatmentHistoryId && a.TreatmentId == TreatmentId)
                                        .OrderByDescending(a => a.LastModifiedDate).FirstOrDefaultAsync();

                if (treatmenthistory != null)
                {
                    _logger.LogInformation("treatmenthistory.id " + treatmenthistory.Id);
                    _logger.LogInformation("SubTreatmentId " + SubTreatmentId);
                    _logger.LogInformation("DeliveryStatus " + DeliveryStatus);

                    var communicationhistorydetails = await _repoFactory.GetRepo().FindAll<TreatmentOnCommunicationHistoryDetails>()
                                                        .Where(a => a.TreatmentHistoryId == treatmenthistory.Id &&
                                                                    a.SubTreatmentId == SubTreatmentId &&
                                                                    DeliveryStatus.Contains(a.WADeliveredStatus))
                                                        .OrderByDescending(a => a.LastModifiedDate).ToListAsync();

                    _logger.LogInformation("communicationhistorydetails count " + communicationhistorydetails.Count());

                    if (communicationhistorydetails != null)
                    {
                        List<string> loanaccountids = new List<string>();

                        loanaccountids = communicationhistorydetails.Select(a => a.LoanAccountId).Distinct().ToList();

                        finalaccountlist = finalaccountlist.Where(a => loanaccountids.Contains(a.id)).ToList();

                        agreementids = finalaccountlist.Select(b => b.agreementid).ToList();
                    }
                }
                return agreementids;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message + " > " + ex.StackTrace + " > " + ex.ToString();
                if (ex.InnerException != null)
                    errorMsg += " >> " + ex.InnerException.Message + " > " + ex.InnerException.StackTrace + " > " + ex.InnerException.ToString();

                _logger.LogInformation("Exception in TreatmentCommonFunctions FetchAccountsForAllocation " + errorMsg);
                return agreementids;
            }
        }

        public async Task<List<string>> FetchTreatmentAllocationIds(string allocationType, string product, string bucket, string region, string state, string city, string tenantId, List<string> designationList, ExecuteTreatmentDto executeTreatmentDto)
        {
            _repoFactory.Init(executeTreatmentDto);
            List<string> allocationIds = new List<string>();

            if (string.Equals(allocationType, AllocationTypeEnum.TeleCallingAgency.Value, StringComparison.OrdinalIgnoreCase))
            {
                allocationIds = await _repoFactory.GetRepo().FindAll<Agency>().IncludePlaceOfWork()
                                    .IncludeAgencyType().IncludeAgencyWorkflow()
                                    .Where(a => a.PlaceOfWork.Any(b => b.Product.Equals(product) &&
                                                                        b.Bucket == bucket &&
                                                                        b.Region.Equals(region) &&
                                                                        b.State.Equals(state) &&
                                                                        b.City.Equals(city)) &&
                                                a.AgencyWorkflowState.Name.Contains("approved") &&
                                                a.AgencyType.SubType.Equals("tele calling"))
                                    .Select(a => a.Id).ToListAsync();
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.FieldAgency.Value))
            {
                allocationIds = await _repoFactory.GetRepo().FindAll<Agency>().IncludePlaceOfWork().IncludeAgencyType().IncludeAgencyWorkflow()
                                    .Where(a => a.PlaceOfWork.Any(b => b.Product.Equals(product) &&
                                                                        b.Bucket == bucket &&
                                                                        b.Region.Equals(region) &&
                                                                        b.State.Equals(state) &&
                                                                        b.City.Equals(city)) &&
                                                a.AgencyWorkflowState.Name.Contains("approved") &&
                                                a.AgencyType.SubType.Equals(AgencySubTypeEnum.TeleCalling.Value))
                                    .Select(a => a.Id).ToListAsync();
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.BankStaff.Value) ||
                     string.Equals(allocationType, AllocationTypeEnum.AllocationOwner.Value))
            {
                allocationIds = await _repoFactory.GetRepo().FindAll<CompanyUser>()//.IncludeCompanyUserScopeOfWork()
                                                                .IncludeCompanyUserWorkflow().IncludeDesignation()
                                    .Where(a => //a.ScopeOfWork.Any(b => b.Product.Equals(product) &&
                                    //                                    b.Bucket == bucket &&
                                    //                                    b.Region.Equals(region) &&
                                    //                                    b.State.Equals(state) &&
                                    //                                    b.City.Equals(city)) &&
                                            a.CompanyUserWorkflowState.Name.Contains("approved") &&
                                            a.Designation.Any(b => designationList.Contains(b.DesignationId)))
                                    .Select(a => a.Id).ToListAsync();
            }
            else if (string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value, StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(allocationType, AllocationTypeEnum.FieldAgent.Value, StringComparison.OrdinalIgnoreCase))
            {
                var agencyUsers = await _repoFactory.GetRepo().FindAll<AgencyUser>()//.IncludeAgencyUserScopeOfWork()
                    .IncludeAgencyUserWorkflow().IncludeAgencyUserDesignation()
                                    .Where(a => //a.ScopeOfWork.Any(b => b.Product.Equals(product) &&
                                    //                                   b.Bucket == bucket &&
                                    //                                   b.Region.Equals(region) &&
                                    //                                   b.State.Equals(state) &&
                                    //                                   b.City.Equals(city)) &&
                                            a.AgencyUserWorkflowState.Name.Contains("approved") &&
                                            a.Designation.Any(b => designationList.Contains(b.DesignationId)))
                                    .Select(a => a.Id).ToListAsync();

                var accountabilityType = string.Equals(allocationType, AllocationTypeEnum.TCAgent.Value) ?
                                         AccountabilityTypeEnum.AgencyToFrontEndExternalTC.Value :
                                         AccountabilityTypeEnum.AgencyToFrontEndExternalFOS.Value;

                var accountabilities = await _repoFactory.GetRepo().FindAll<Accountability>()
                                                   .Where(a => agencyUsers.Contains(a.ResponsibleId) &&
                                                               a.AccountabilityTypeId.Equals(accountabilityType))
                                                   .ToListAsync();

                var responsibleIds = accountabilities.Select(a => a.ResponsibleId).ToList();
                allocationIds = agencyUsers.Where(a => responsibleIds.Contains(a)).ToList();
            }

            return allocationIds;
        }

    }

    public class TreatmentAllocationDetail
    {
        public string AllocationId { get; set; }

        public string Percentage { get; set; }

        public double Count { get; set; }

        public double? Amount { get; set; }

        public string RuleName { get; set; }
    }

    public class TreatmentUsersName
    {
        public string AllocationId { get; set; }
        public string Branch { get; set; }
    }

    public class TreatmentUsersSubProductName
    {
        public string AllocationId { get; set; }
        public string SubproductId { get; set; }
        public string SubproductName { get; set; }
    }

    public class TreatmentPincodeUsersName
    {
        public string AllocationId { get; set; }
        public string AreaCode { get; set; }
    }

    public class TreatmentCustomerPersonaUsersName
    {
        public string AllocationId { get; set; }
        public string CustomerPersona { get; set; }
    }

    public class TreatmentLoanAccountDetails
    {
        public string Id { get; set; }
        public string AgreementId { get; set; }
        public string AllocationOwnerId { get; set; }
        public string TelecallerAgencyId { get; set; }
        public string FieldAgencyId { get; set; }
        public string TelecallerAgentId { get; set; }
        public string AgentId { get; set; }
        public string Product { get; set; }
        public string Bucket { get; set; }
        public string Region { get; set; }

        public string State { get; set; }
        public string City { get; set; }
        public string Branch { get; set; }

        public string StaffCode { get; set; }

        public bool? IsMatched { get; set; }
    }

    public class UserSOW
    {
        public string Id { get; set; }
        public string Product { get; set; }

        public string Bucket { get; set; }

        public string Region { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Branch { get; set; }

        public string StaffCode { get; set; }
    }

    public class AgencyUserPOW
    {
        public string Id { get; set; }
        public string AreaCode { get; set; }
    }

    public class CompanyUserPOW
    {
        public string Id { get; set; }
        public string AreaCode { get; set; }
    }

    public class ApplicationUserSkill
    {
        public string Id { get; set; }
        public string CustomerPersona { get; set; }
    }

    public class UserLoadCheck
    {
        public string UserId { get; set; }
        public int? MaxLoad { get; set; }

        public int? CurrentLoad { get; set; }
    }

    public class AndConditionCheck
    {
        public bool IsBranchCondition { get; set; }

        public bool IsPincodeCondition { get; set; }

        public bool IsSubProductCondition { get; set; }

        public bool IsPersonaCondition { get; set; }
    }

    public class UserWithAllCondition
    {
        public string AllocationId { get; set; }

        public string Pincode { get; set; }

        public string Branch { get; set; }

        public string SubProduct { get; set; }

        public string CustomerPersona { get; set; }
    }
}