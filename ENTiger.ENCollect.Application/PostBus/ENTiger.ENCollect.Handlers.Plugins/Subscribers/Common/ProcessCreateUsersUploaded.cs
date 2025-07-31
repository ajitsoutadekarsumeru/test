using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.IO.Abstractions;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using ENCollect.Security;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCreateUsersUploaded : IProcessCreateUsersUploaded
    {
        protected readonly ILogger<ProcessCreateUsersUploaded> _logger;
        IFlexServiceBusBridge _bus;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IDataTableUtility _dataTableUtility;
        protected readonly IFlexPrimaryKeyGeneratorBridge _pkGenerator;
        protected readonly ICustomUtility _customUtility;
        private readonly ICsvExcelUtility _csvExcelUtility;
        private readonly FilePathSettings _fileSettings;
        private readonly DatabaseSettings _databaseSettings;
        private readonly IFileSystem _fileSystem;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        private readonly IFileTransferUtility _fileTransferUtility;
        UsersCreateFile? model;
        ApplicationUser? user;
        public ProcessCreateUsersUploaded(ILogger<ProcessCreateUsersUploaded> logger, IRepoFactory repoFactory, IFlexServiceBusBridge bus, IFlexPrimaryKeyGeneratorBridge pkGenerator, ICsvExcelUtility csvExcelUtility, IOptions<FilePathSettings> fileSettings,
            IOptions<DatabaseSettings> databaseSettings, IFileSystem fileSystem, IOptions<FileConfigurationSettings> fileConfigurationSettings, IDataTableUtility dataTableUtility,
            ICustomUtility customUtility, IFileTransferUtility fileTransferUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _bus = bus;
            _pkGenerator = pkGenerator;
            _dataTableUtility = dataTableUtility;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _customUtility = customUtility;
            _fileSystem = fileSystem;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
            _fileTransferUtility = fileTransferUtility;
        }

        public virtual async Task Execute(CreateUsersUploadedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            try
            {
                string _filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
                string tempPath = _fileSystem.Path.Combine(_filepath, _fileSettings.TemporaryPath);
                string processedFilePath = _fileSystem.Path.Combine(_filepath, _fileSettings.UserProcessedFilePath);
                string DBType = _databaseSettings.DBType;
                string _sheetName = _fileConfigurationSettings.DefaultSheet;
                _flexAppContext = @event.AppContext; //do not remove this line
                string userId = _flexAppContext.UserId;
                string tenantId = _flexAppContext.TenantId;
                var repo = _repoFactory.Init(@event);
                _logger.LogInformation("ProcessCreateUsersUploaded : Start");
                _logger.LogInformation("ProcessCreateUsersUploaded : JSON - " + JsonConvert.SerializeObject(@event));

                model = await _repoFactory.GetRepo().FindAll<UsersCreateFile>().Where(x => x.Id == @event.Id).FirstOrDefaultAsync();

                user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == model.CreatedBy).FirstOrDefaultAsync();

                var dataTable = _dataTableUtility.ExcelToDataTable(_filepath, model.FileName, _sheetName);

                bool IsCorrectFileHeader = await ValidateHeadersAsync(dataTable, model.UploadType);

                if (IsCorrectFileHeader)
                {
                    dataTable.Columns.Add("Id", typeof(string)).SetOrdinal(0);
                    dataTable.Columns.Add("IsFailed", typeof(string));
                    dataTable.Columns.Add("FailureReason", typeof(string));

                    if (string.Equals(model.UploadType, UploadTypeEnum.Agent.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        await ProcessAgentData(dataTable);
                    }
                    else if (string.Equals(model.UploadType, UploadTypeEnum.Agency.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        await ProcessAgencyData(dataTable);
                    }
                    else if (string.Equals(model.UploadType, UploadTypeEnum.Staff.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        await ProcessStaffDataAsync(dataTable);
                    }

                    #region UpdateStatus

                    string status = FetchStatus(dataTable);
                    model.Status = status;
                    model.SetLastModifiedDate(DateTime.Now);
                    model.SetAddedOrModified();

                    _repoFactory.GetRepo().InsertOrUpdate(model);
                    int records = await _repoFactory.GetRepo().SaveAsync();
                    #endregion UpdateStatus

                    #region Copy DataTable to CSV
                    string filename = model.CustomId + ".csv";
                    string filePath = Path.Combine(processedFilePath, filename);
                    _csvExcelUtility.ToCSV(dataTable, filePath);

                    // Move the processed file from the incoming path to the user processed folder, overwriting if the file already exists
                    await _fileTransferUtility.RenameAndMoveFileAsync(_filepath, processedFilePath, model.FileName);

                    #endregion Copy DataTable to CSV

                    EventCondition = CONDITION_ONSUCCESS;
                }
                else
                {
                    model.Status = FileStatusEnum.InvalidFileFormat.Value;
                    model.SetLastModifiedDate(DateTime.Now);
                    model.SetAddedOrModified();

                    _repoFactory.GetRepo().InsertOrUpdate(model);
                    int records = await _repoFactory.GetRepo().SaveAsync();

                    #region Copy DataTable to CSV
                    string filename = model.CustomId + ".csv";
                    string filePath = Path.Combine(processedFilePath, filename);
                    _csvExcelUtility.ToCSV(dataTable, filePath);
                    #endregion Copy DataTable to CSV

                    EventCondition = CONDITION_ONFAILURE;
                }

                _logger.LogInformation("ProcessCreateUsersUploaded : End | EventCondition - " + EventCondition);
                await this.Fire<ProcessCreateUsersUploaded>(EventCondition, serviceBusContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ProcessCreateUsersUploaded " + ex);
            }
        }

        private string FetchStatus(DataTable dataTable)
        {
            string status = string.Empty;
            int failedcount = dataTable.Select("IsFailed = True").Count();
            int successcount = dataTable.Select("IsFailed = False").Count();
            int totalcount = dataTable.Rows.Count;

            if (totalcount == 0)
            {
                status = FileStatusEnum.Failed.Value;
            }
            else if (totalcount == successcount)
            {
                status = FileStatusEnum.Processed.Value;
            }
            else if (totalcount == failedcount)
            {
                status = FileStatusEnum.Failed.Value;
            }
            else if (successcount > 0 && successcount < totalcount)
            {
                status = FileStatusEnum.Partial.Value;
            }

            return status;
        }

        private async Task ProcessAgentData(DataTable dataTable)
        {
            var duplicateMobileNumber = dataTable.AsEnumerable().GroupBy(a => a["MobileNo"]).Where(gr => gr.Count() > 1).ToList();
            var duplicateEmailId = dataTable.AsEnumerable().GroupBy(a => a["EmailId"]).Where(gr => gr.Count() > 1).ToList();

            if (dataTable.Rows.Count == 0)
            {
                // Option 1: Add a row manually with failure reason
                DataRow newRow = dataTable.NewRow();
                newRow["Id"] = SequentialGuid.NewGuidString();
                newRow["IsFailed"] = "True";
                newRow["FailureReason"] = "The uploaded file contains no data.";
                dataTable.Rows.Add(newRow);

                // Optionally return or log here
                return; // or continue if you want to allow further execution
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                List<BulkUserErrorList> errorlist = new List<BulkUserErrorList>();
                AddAgentDto addAgentDto = await ValidateAndConstructAgentModelAsync(dataTable, errorlist, i);


                var mobilecheck = duplicateMobileNumber?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addAgentDto.PrimaryMobileNumber).FirstOrDefault();

                if (mobilecheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 13;
                    error.errormessage = "Duplicate MobileNumber in File";
                    errorlist.Add(error);
                }

                var emailcheck = duplicateEmailId?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addAgentDto.PrimaryEMail).FirstOrDefault();

                if (emailcheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 14;
                    error.errormessage = "Duplicate EmailId in File";
                    errorlist.Add(error);
                }

                ValidateAddAgentUtility validateAddAgentUtility = new ValidateAddAgentUtility();
                var validateerrorlist = await validateAddAgentUtility.ValidateAddAgentAsync(addAgentDto);

                foreach (var err in validateerrorlist)
                {
                    errorlist.Add(err);
                }

                if (errorlist.Count() > 0)
                {
                    string errormessage = "";

                    foreach (var error in errorlist.OrderBy(a => a.sequence))
                    {
                        errormessage = errormessage + " | " + error.errormessage;
                    }

                    dataTable.Rows[i]["Id"] = SequentialGuid.NewGuidString();
                    dataTable.Rows[i]["IsFailed"] = "True";
                    dataTable.Rows[i]["FailureReason"] = errormessage;
                }
                else
                {
                    addAgentDto.PrimaryEMail = addAgentDto.PrimaryEMail.ToLower();
                    addAgentDto.SupervisorEmailId = addAgentDto.SupervisorEmailId?.ToLower();
                    AddAgentCommand cmd = new AddAgentCommand();
                    cmd.Dto = addAgentDto;
                    await _bus.Send(cmd);

                    dataTable.Rows[i]["Id"] = SequentialGuid.NewGuidString();
                    dataTable.Rows[i]["IsFailed"] = "False";
                }
            }
        }

        private async Task<AddAgentDto> ValidateAndConstructAgentModelAsync(DataTable dataTable, List<BulkUserErrorList> errorlist, int i)
        {
            AddAgentDto addAgentDto = new AddAgentDto();
            addAgentDto.Roles = new List<UserDesignationDto>();
            UserDesignationDto role = new UserDesignationDto();

            Agency? agency;
            Department? department;
            Designation? designation;
            string agencycode = dataTable.Rows[i]["AgencyCode"].ToString();

            if (string.IsNullOrEmpty(agencycode))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 4;
                error.errormessage = "Agency Code is Required";

                errorlist.Add(error);
            }
            else
            {
                agency = await _repoFactory.GetRepo().FindAll<Agency>().IncludeAgencyWorkflow()
                                    .Where(a => a.CustomId == agencycode &&
                                            a.AgencyWorkflowState != null && a.AgencyWorkflowState.Name == "AgencyApproved")
                                    .FirstOrDefaultAsync();

                if (agency == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 4;
                    error.errormessage = "Agency Code does not exist or not approved ";
                    errorlist.Add(error);
                }
                else
                {
                    addAgentDto.AgencyId = agency?.Id;
                }
            }

            string departmentname = dataTable.Rows[i]["Department"].ToString();

            if (string.IsNullOrEmpty(departmentname))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 10;
                error.errormessage = "Department is Required";

                errorlist.Add(error);
            }
            else
            {
                department = await _repoFactory.GetRepo().FindAll<Department>().Where(a => a.Name == departmentname).FirstOrDefaultAsync();
                if (department == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 10;
                    error.errormessage = "Department is invalid";
                    errorlist.Add(error);
                }
                else
                {
                    role.DepartmentId = department?.Id;
                }
            }

            string designationname = dataTable.Rows[i]["Designation"].ToString();

            if (string.IsNullOrEmpty(designationname))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 11;
                error.errormessage = "Designation is Required";

                errorlist.Add(error);
            }
            else
            {
                designation = await _repoFactory.GetRepo().FindAll<Designation>().Where(a => a.Name == designationname).FirstOrDefaultAsync();

                if (designation == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 11;
                    error.errormessage = "Designation is invalid";
                    errorlist.Add(error);
                }
                else
                {
                    role.DesignationId = designation?.Id;
                }
            }

            string DRACertificateDate = dataTable.Rows[i]["DRACertificationDate"].ToString();

            DateTime DRACertificateDateAsDate;

            _logger.LogInformation("DRACertificateDate " + DRACertificateDate);
            bool? IsDRACertificateDate = false;

            if (!string.IsNullOrEmpty(DRACertificateDate))
            {
                BulkUserDateCheck bulkUserDateCheck = IsDateValid(DRACertificateDate);
                if (bulkUserDateCheck.validate)
                {
                    IsDRACertificateDate = true;
                    DRACertificateDateAsDate = bulkUserDateCheck.Date;

                    if (DRACertificateDateAsDate > DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 7;
                        error.errormessage = "DRA Certification Date cannot be future date";

                        errorlist.Add(error);
                    }
                    addAgentDto.DRACertificateDate = DRACertificateDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 7;
                    error.errormessage = "DRA Certificate date is invalid Dateformat";

                    errorlist.Add(error);
                }
            }

            string DRATrainingDate = dataTable.Rows[i]["DRATrainingDate"].ToString();
            bool? IsDRATrainingDate = false;
            DateTime DRATrainingDateAsDate;

            if (!string.IsNullOrEmpty(DRATrainingDate))
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();
                bulkUserDateCheck = IsDateValid(DRATrainingDate);
                if (bulkUserDateCheck.validate)
                {
                    IsDRATrainingDate = true;
                    DRATrainingDateAsDate = bulkUserDateCheck.Date;

                    if (DRATrainingDateAsDate > DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 8;
                        error.errormessage = "DRA Training Date cannot be future";

                        errorlist.Add(error);
                    }

                    addAgentDto.DRATrainingDate = DRATrainingDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 8;
                    error.errormessage = "DRA Training date is invalid Dateformat";

                    errorlist.Add(error);
                }
            }

            string DateOfBirth = dataTable.Rows[i]["DateOfBirth"].ToString();
            bool? IsDateOfBirth = false;
            DateTime DateOfBirthAsDate;

            if (string.IsNullOrEmpty(DateOfBirth))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 12;
                error.errormessage = "Date Of Birth is required";

                errorlist.Add(error);
            }

            else if (!string.IsNullOrEmpty(DateOfBirth))
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();

                bulkUserDateCheck = IsDateValid(DateOfBirth);
                if (bulkUserDateCheck.validate)
                {
                    IsDateOfBirth = true;
                    DateOfBirthAsDate = bulkUserDateCheck.Date;

                    if (DateOfBirthAsDate > DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 12;
                        error.errormessage = "Date Of Birth is invalid";

                        errorlist.Add(error);
                    }
                    addAgentDto.DateOfBirth = DateOfBirthAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 12;
                    error.errormessage = "Date Of Birth is invalid Dateformat";

                    errorlist.Add(error);
                }
            }

            string LastRenewalDate = dataTable.Rows[i]["LastRenewalDate"].ToString();
            bool? IsLastRenewalDate = false;
            DateTime LastRenewalDateAsDate;

            if (string.IsNullOrEmpty(LastRenewalDate))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 19;
                error.errormessage = "Last Renewal date is required";

                errorlist.Add(error);
            }
            else if (LastRenewalDate != null)
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();

                bulkUserDateCheck = IsDateValid(LastRenewalDate);
                if (bulkUserDateCheck.validate)
                {
                    IsLastRenewalDate = true;
                    LastRenewalDateAsDate = bulkUserDateCheck.Date;
                    if (LastRenewalDateAsDate > DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 19;
                        error.errormessage = "Last Renewal date cannot be future date.";

                        errorlist.Add(error);
                    }
                    addAgentDto.LastRenewalDate = LastRenewalDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 19;
                    error.errormessage = "Last Renewal Date is invalid Dateformat";

                    errorlist.Add(error);
                }
            }

            string CollectionEmploymentDate = dataTable.Rows[i]["CollectionEmploymentDate"].ToString();
            bool? IsCollectionEmploymentDate = false;
            DateTime CollectionEmploymentDateAsDate;

            if (string.IsNullOrEmpty(CollectionEmploymentDate))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 18;
                error.errormessage = "Collection Employment Date is required";
                errorlist.Add(error);
            }
            else if (CollectionEmploymentDate != null)
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();

                bulkUserDateCheck = IsDateValid(CollectionEmploymentDate);
                if (bulkUserDateCheck.validate)
                {
                    IsCollectionEmploymentDate = true;
                    CollectionEmploymentDateAsDate = bulkUserDateCheck.Date;

                    if (CollectionEmploymentDateAsDate > DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 18;
                        error.errormessage = "Collection Employment Date cannot be future date.";
                        errorlist.Add(error);
                    }
                    addAgentDto.EmploymentDate = CollectionEmploymentDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 18;
                    error.errormessage = "Collection Employment Date is invalid Dateformat";
                    errorlist.Add(error);
                }
            }

            string AuthorizationCardExpiryDate = dataTable.Rows[i]["AuthorizationCardExpiryDate"].ToString();
            bool? IsAuthorizationCardExpiryDate = false;
            DateTime AuthorizationCardExpiryDateAsDate;

            if (string.IsNullOrEmpty(AuthorizationCardExpiryDate))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 20;
                error.errormessage = "Authorization Card Expiry Date is required";
                errorlist.Add(error);
            }
            else if (AuthorizationCardExpiryDate != null)
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();

                bulkUserDateCheck = IsDateValid(AuthorizationCardExpiryDate);
                if (bulkUserDateCheck.validate)
                {
                    IsAuthorizationCardExpiryDate = true;
                    AuthorizationCardExpiryDateAsDate = bulkUserDateCheck.Date;

                    if (AuthorizationCardExpiryDateAsDate < DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 20;
                        error.errormessage = "Authorization card expiry cannot be of past date";
                        errorlist.Add(error);
                    }
                    addAgentDto.AuthorizationCardExpiryDate = AuthorizationCardExpiryDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 20;
                    error.errormessage = "Authorization Card Expiry Date is invalid Dateformat";
                    errorlist.Add(error);
                }
            }
            string userType = dataTable.Rows[i]["UserType"].ToString();
            if (string.IsNullOrEmpty(userType))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 1;
                error.errormessage = "User Type is Required";
                errorlist.Add(error);
            }
            else
            {
                bool isValidUserType = Enum.GetNames(typeof(UserTypeEnum))
                           .Any(e => e.Equals(userType, StringComparison.OrdinalIgnoreCase));

                if (!isValidUserType)
                {
                    errorlist.Add(new BulkUserErrorList
                    {
                        sequence = 1,
                        errormessage = $"Invalid User Type: '{userType}'. User Type must be one of: FOS, Telecaller, or Others."
                    });
                }
                else
                {
                    string userTypeNormalized = Enum.GetNames(typeof(UserTypeEnum)).FirstOrDefault(e => e.Equals(userType, StringComparison.OrdinalIgnoreCase));
                    var parsedUserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), userTypeNormalized);
                    addAgentDto.UserType = parsedUserType.ToString();
                }

            }
            addAgentDto.SetGeneratedId(_pkGenerator.GenerateKey());
            addAgentDto.isSaveAsDraft = true;
            addAgentDto.FirstName = dataTable.Rows[i]["FirstName"].ToString();
            addAgentDto.LastName = dataTable.Rows[i]["LastName"].ToString();

            addAgentDto.SupervisorEmailId = dataTable.Rows[i]["SupervisorEmailId"].ToString();
            addAgentDto.DiallerId = dataTable.Rows[i]["DialerId"].ToString();

            addAgentDto.DRAUniqueRegistrationNumber = dataTable.Rows[i]["DRAUniqueRegistrationNumber"].ToString();

            addAgentDto.PrimaryMobileNumber = dataTable.Rows[i]["MobileNo"].ToString();
            addAgentDto.PrimaryEMail = dataTable.Rows[i]["EmailId"].ToString();
            addAgentDto.FatherName = dataTable.Rows[i]["FathersName"].ToString();

            addAgentDto.Remarks = dataTable.Rows[i]["Remarks"].ToString();

            addAgentDto.Roles.Add(role);

            addAgentDto.Address = new AddressDto();
            addAgentDto.Address.AddressLine = dataTable.Rows[i]["LocalResidentialAddress"].ToString();
            addAgentDto.Address.PIN = dataTable.Rows[i]["PostalCode"].ToString();
            addAgentDto.SetAppContext(_flexAppContext);

            addAgentDto.profileIdentification = new List<AgencyUserIdentificationDto>();            
            addAgentDto.Languages = new List<LanguageDto>();
            addAgentDto.CreditAccountDetails = new CreditAccountDetailsDto();
            addAgentDto.ProductScopes = new List<UserProductScopeDto>();
            addAgentDto.GeoScopes = new List<UserGeoScopeDto>();
            addAgentDto.BucketScopes = new List<UserBucketScopeDto>();
            addAgentDto.PlaceOfWork = new List<AgencyUserPlaceOfWorkDto>();
            return addAgentDto;
        }

        private async Task ProcessAgencyData(DataTable dataTable)
        {
            var duplicateMobileNumber = dataTable.AsEnumerable().GroupBy(a => a["MobileNo"]).Where(gr => gr.Count() > 1).ToList();
            var duplicateEmailId = dataTable.AsEnumerable().GroupBy(a => a["EmailID"]).Where(gr => gr.Count() > 1).ToList();
            var duplicateAgencyCode = dataTable.AsEnumerable().GroupBy(a => a["AgencyCode"]).Where(gr => gr.Count() > 1).ToList();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                List<BulkUserErrorList> errorlist = new List<BulkUserErrorList>();
                AddAgencyDto addAgencyDto = await ValidateAndConstructAgencyModel(dataTable, i, errorlist);

                var mobilecheck = duplicateMobileNumber?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addAgencyDto.PrimaryMobileNumber).FirstOrDefault();

                if (mobilecheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 14;
                    error.errormessage = "Duplicate MobileNumber in File";
                    errorlist.Add(error);
                }

                var emailcheck = duplicateEmailId?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addAgencyDto.PrimaryEMail).FirstOrDefault();

                if (emailcheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 15;
                    error.errormessage = "Duplicate EmailId in File";
                    errorlist.Add(error);
                }


                var agencycodecheck = duplicateAgencyCode?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addAgencyDto.AgencyCode).FirstOrDefault();

                if (emailcheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 3;
                    error.errormessage = "Duplicate AgencyCode in File";
                    errorlist.Add(error);
                }

                ValidateAddAgencyUtility validateAddAgecytUtility = new ValidateAddAgencyUtility();
                var validateerrorlist = await validateAddAgecytUtility.ValidateAddAgencyAsync(addAgencyDto);

                foreach (var err in validateerrorlist)
                {
                    errorlist.Add(err);
                }

                if (errorlist.Count() > 0)
                {
                    string errormessage = "";

                    foreach (var error in errorlist.OrderBy(a => a.sequence))
                    {
                        errormessage = errormessage + " | " + error.errormessage;
                    }
                    dataTable.Rows[i]["Id"] = SequentialGuid.NewGuidString();
                    dataTable.Rows[i]["IsFailed"] = "True";
                    dataTable.Rows[i]["FailureReason"] = errormessage;
                }
                else
                {
                    AddAgencyCommand cmd = new AddAgencyCommand();
                    cmd.Dto = addAgencyDto;
                    await _bus.Send(cmd);

                    dataTable.Rows[i]["Id"] = SequentialGuid.NewGuidString();
                    dataTable.Rows[i]["IsFailed"] = "False";
                }
            }
        }

        private async Task<AddAgencyDto> ValidateAndConstructAgencyModel(DataTable dataTable, int i, List<BulkUserErrorList> errorlist)
        {
            bool parentagencyValue = false;
            string? parentagencyid = "";
            string? agencytypeid = "";
            string recommendingofficerid = "";

            AddAgencyDto addAgencyDto = new AddAgencyDto();

            string isparentagency = dataTable.Rows[i]["IsParentAgency"].ToString();

            string ParentAgencyCode = dataTable.Rows[i]["ParentAgencyCode"].ToString();

            string RecommendingOfficer = dataTable.Rows[i]["RecommendingOfficer"].ToString();

            string AgencyType = dataTable.Rows[i]["AgencyType"].ToString();

            string SubType = dataTable.Rows[i]["AgencySubType"].ToString();

            if (string.IsNullOrEmpty(isparentagency))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 1;
                error.errormessage = "Is Parent agency is required Required";
                errorlist.Add(error);
            }
            else
            {
                if (isparentagency.Length > 3)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 1;
                    error.errormessage = "Is parent Agency cannot be more than 3 characters.";
                    errorlist.Add(error);
                }
                if (string.Equals(isparentagency, "no", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(ParentAgencyCode))
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 2;
                        error.errormessage = "Parent Agency Code is required";
                        errorlist.Add(error);
                    }
                    if (!string.IsNullOrEmpty(ParentAgencyCode))
                    {
                        var parentagency = await _repoFactory.GetRepo().FindAll<Agency>()
                                                .Where(a => a.CustomId == ParentAgencyCode && a.IsParentAgency == true)
                                                .FirstOrDefaultAsync();
                        if (parentagency != null)
                        {
                            BulkUserErrorList error = new BulkUserErrorList();
                            error.sequence = 2;
                            error.errormessage = "Parent Agency code should exist in DB and should belong to parent agency.";
                            errorlist.Add(error);
                        }
                    }
                }
                if (string.Equals(isparentagency, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    parentagencyValue = true;
                    if (!string.IsNullOrEmpty(ParentAgencyCode))
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 2;
                        error.errormessage = "Parent Agency Code is not required";
                        errorlist.Add(error);
                    }
                }
            }

            if (string.IsNullOrEmpty(RecommendingOfficer))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 5;
                error.errormessage = "Recommending officer  is Required";
                errorlist.Add(error);
            }
            else
            {
                if (RecommendingOfficer.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 5;
                    error.errormessage = "RecommendingOfficer cannot be more than 40 characters";
                    errorlist.Add(error);
                }
                var companyuser = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                        .Where(a => a.FirstName == RecommendingOfficer || a.CustomId == RecommendingOfficer)
                                        .FirstOrDefaultAsync();
                if (companyuser == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 5;
                    error.errormessage = "RecommendingOfficer is invalid";
                    errorlist.Add(error);
                }
                else
                {
                    recommendingofficerid = companyuser.Id;
                }
            }

            if (string.IsNullOrEmpty(AgencyType))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 6;
                error.errormessage = "Agency Type is Required";
                errorlist.Add(error);
            }
            else
            {
                if (!Regex.IsMatch(AgencyType, @"^[\sa-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 6;
                    error.errormessage = "AgencyType should not contain special character.";
                    errorlist.Add(error);
                }
                if (AgencyType.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 6;
                    error.errormessage = "Agency Type cannot be more than 40 characters";
                    errorlist.Add(error);
                }
                var agencyType = await _repoFactory.GetRepo().FindAll<AgencyType>().Where(a => a.MainType == AgencyType).FirstOrDefaultAsync();
                if (agencyType == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 6;
                    error.errormessage = "Agency Type is invalid";
                    errorlist.Add(error);
                }
            }

            if (string.IsNullOrEmpty(SubType))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 7;
                error.errormessage = "Agency Type is Required";
                errorlist.Add(error);
            }
            else
            {
                if (!Regex.IsMatch(SubType, @"^[\sa-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 7;
                    error.errormessage = "Sub Type should not contain special character.";
                    errorlist.Add(error);
                }
                if (SubType.Length > 40)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 7;
                    error.errormessage = "Sub Type cannot be more than 40 characters";
                    errorlist.Add(error);
                }
                var agencyType = await _repoFactory.GetRepo().FindAll<AgencyType>().Where(a => a.SubType == SubType).FirstOrDefaultAsync();
                if (agencyType == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 7;
                    error.errormessage = "Sub Type is invalid";
                    errorlist.Add(error);
                }
            }

            string LastRenewalDate = dataTable.Rows[i]["LastRenewalDate"].ToString();
            bool? IsLastRenewalDate = false;
            DateTime LastRenewalDateAsDate;

            if (string.IsNullOrEmpty(LastRenewalDate))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 17;
                error.errormessage = "Last Renewal date is required";
                errorlist.Add(error);
            }
            else
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();

                bulkUserDateCheck = IsDateValid(LastRenewalDate);
                if (bulkUserDateCheck.validate)
                {
                    IsLastRenewalDate = true;
                    LastRenewalDateAsDate = bulkUserDateCheck.Date;

                    if (LastRenewalDateAsDate > DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 17;
                        error.errormessage = "Last Renewal date cannot be future date.";
                        errorlist.Add(error);
                    }
                    addAgencyDto.LastRenewalDate = LastRenewalDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 17;
                    error.errormessage = "Last Renewal Date is invalid Dateformat";
                    errorlist.Add(error);
                }
            }

            string ContractExpiryDate = dataTable.Rows[i]["ContractExpiryDate"].ToString();
            _logger.LogInformation("ContractExpiryDate " + ContractExpiryDate);
            bool? IsContractExpiryDate = false;
            DateTime ContractExpiryDateAsDate;

            if (string.IsNullOrEmpty(ContractExpiryDate))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 18;
                error.errormessage = "Contract Expiry Date is required";
                errorlist.Add(error);
            }
            else
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();
                bulkUserDateCheck = IsDateValid(ContractExpiryDate);
                if (bulkUserDateCheck.validate)
                {
                    IsContractExpiryDate = true;
                    ContractExpiryDateAsDate = bulkUserDateCheck.Date;

                    if (ContractExpiryDateAsDate < DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 18;
                        error.errormessage = "Contract expiry date cannot be past date.";
                        errorlist.Add(error);
                    }
                    addAgencyDto.ContractExpireDate = ContractExpiryDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 18;
                    error.errormessage = "Contract Expiry Date is invalid Dateformat";
                    errorlist.Add(error);
                }
            }

            string FirstAgreementDate = dataTable.Rows[i]["FirstAgreementDate"].ToString();
            bool? IsFirstAgreementDate = false;
            DateTime FirstAgreementDateAsDate;

            if (string.IsNullOrEmpty(FirstAgreementDate))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 16;
                error.errormessage = "First Agreement Date is required";
                errorlist.Add(error);
            }
            else
            {
                BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();
                bulkUserDateCheck = IsDateValid(FirstAgreementDate);
                if (bulkUserDateCheck.validate)
                {
                    IsFirstAgreementDate = true;
                    FirstAgreementDateAsDate = bulkUserDateCheck.Date;

                    if (FirstAgreementDateAsDate > DateTime.Now)
                    {
                        BulkUserErrorList error = new BulkUserErrorList();
                        error.sequence = 16;
                        error.errormessage = "First Agreement Date cannot be future date.";
                        errorlist.Add(error);
                    }
                    addAgencyDto.FirstAgreementDate = FirstAgreementDateAsDate;
                }

                if (!bulkUserDateCheck.validate)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 16;
                    error.errormessage = "First Agreement Date is invalid Dateformat";
                    errorlist.Add(error);
                }
            }

            string PAN = dataTable.Rows[i]["PanCard"].ToString();
            bool IsPANHasError = false;
            string EncryptedPAN = string.Empty;

            if (string.IsNullOrEmpty(PAN))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 9;
                error.errormessage = "PAN is required Required";
                errorlist.Add(error);

                IsPANHasError = true;
            }
            else
            {
                if (!Regex.IsMatch(PAN, @"^[a-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 9;
                    error.errormessage = "PAN Number Should not contain special character";
                    errorlist.Add(error);

                    IsPANHasError = true;
                }

                if (PAN.Length > 10)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 9;
                    error.errormessage = "PAN Number can not  be more than 10 characters";
                    errorlist.Add(error);

                    IsPANHasError = true;
                }
                if (PAN.Length < 10)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 9;
                    error.errormessage = "Pancard Number can not  be less than 10 characters";
                    errorlist.Add(error);

                    IsPANHasError = true;
                }
            }

            if (IsPANHasError == false)
            {
                var panBytes = System.Text.Encoding.UTF8.GetBytes(PAN);
                EncryptedPAN = Convert.ToBase64String(panBytes);
            }


            addAgencyDto.AgencyCode = dataTable.Rows[i]["AgencyCode"].ToString();

            if (!string.IsNullOrEmpty(addAgencyDto.AgencyCode))
            {
                if (!Regex.IsMatch(addAgencyDto.AgencyCode, @"^[a-zA-Z0-9]+$"))
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 3;
                    error.errormessage = "AgencyCode should not contain special character.";
                    errorlist.Add(error);
                }

                var agencycodevalue = await _repoFactory.GetRepo().FindAll<Agency>()
                                                    .Where(a => a.CustomId == addAgencyDto.AgencyCode)
                                                    .FirstOrDefaultAsync();
                if (agencycodevalue != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 3;
                    error.errormessage = "AgencyCode should be unique.";
                    errorlist.Add(error);
                }
            }
            else
            {
                addAgencyDto.AgencyCode = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.Agency.Value);
            }

            var parentAgency = await _repoFactory.GetRepo().FindAll<Agency>()
                                .Where(a => a.CustomId == ParentAgencyCode)
                                .FirstOrDefaultAsync();
            parentagencyid = parentAgency?.Id;

            var agencyTypeObj = await _repoFactory.GetRepo().FindAll<AgencyType>()
                                    .Where(a => string.Equals(a.MainType, AgencyType) && string.Equals(a.SubType, SubType))
                                    .FirstOrDefaultAsync();
            agencytypeid = agencyTypeObj?.Id;

            addAgencyDto.SetGeneratedId(_pkGenerator.GenerateKey());
            addAgencyDto.isSaveAsDraft = true;
            addAgencyDto.IsParentAgency = parentagencyValue;
            addAgencyDto.ParentAgencyId = parentagencyid;
            addAgencyDto.RecommendingOfficerId = recommendingofficerid;

            addAgencyDto.FirstName = dataTable.Rows[i]["AgencyName"].ToString();
            addAgencyDto.CollectionAgencyTypeId = agencytypeid;
            addAgencyDto.TIN = dataTable.Rows[i]["TINNumber"].ToString();
            addAgencyDto.PAN = !string.IsNullOrEmpty(EncryptedPAN) ? EncryptedPAN : null;
            addAgencyDto.PrimaryOwnerFirstName = dataTable.Rows[i]["PrimaryOwnerFirstName"].ToString();
            addAgencyDto.PrimaryOwnerLastName = dataTable.Rows[i]["PrimaryOwnerLastName"].ToString();
            addAgencyDto.PrimaryOwnerLastName = dataTable.Rows[i]["PrimaryOwnerLastName"].ToString();
            addAgencyDto.PrimaryMobileNumber = dataTable.Rows[i]["MobileNo"].ToString();
            addAgencyDto.PrimaryEMail = dataTable.Rows[i]["EmailID"].ToString();

            addAgencyDto.Remarks = dataTable.Rows[i]["Remarks"].ToString();
            addAgencyDto.isOrganization = true;

            addAgencyDto.Address = new AddressDto();
            addAgencyDto.Address.AddressLine = dataTable.Rows[i]["RegisteredAgencyAddress"].ToString();
            addAgencyDto.Address.LandMark = dataTable.Rows[i]["Landmark"].ToString();

            addAgencyDto.CreditAccountDetails = new CreditAccountDetailsDto();

            addAgencyDto.ScopeOfWork = new List<AgencyScopeOfWorkDto>();
            addAgencyDto.PlaceOfWork = new List<AgencyPlaceOfWorkDto>();
            addAgencyDto.ProfileIdentification = new List<AgencyIdentificationDto>();

            addAgencyDto.ProfileIdentification = AssignDocuments();

            addAgencyDto.SetAppContext(_flexAppContext);
            return addAgencyDto;
        }

        private List<AgencyIdentificationDto> AssignDocuments()
        {
            List<AgencyIdentificationDto> ProfileIdentification = new List<AgencyIdentificationDto>();

            AgencyIdentificationDto identificationDto1 = new AgencyIdentificationDto();
            identificationDto1.Id = "";
            identificationDto1.IsWavedOff = false;
            identificationDto1.IsDeferred = false;
            identificationDto1.IdentificationTypeId = "16";
            identificationDto1.IdentificationDocTypeId = "33";
            identificationDto1.IdentificationDocId = "";

            ProfileIdentification.Add(identificationDto1);

            AgencyIdentificationDto identificationDto2 = new AgencyIdentificationDto();
            identificationDto2.Id = "";
            identificationDto2.IsWavedOff = false;
            identificationDto2.IsDeferred = false;
            identificationDto2.IdentificationTypeId = "17";
            identificationDto2.IdentificationDocTypeId = "17";
            identificationDto2.IdentificationDocId = "";

            ProfileIdentification.Add(identificationDto2);

            AgencyIdentificationDto identificationDto3 = new AgencyIdentificationDto();
            identificationDto3.Id = "";
            identificationDto3.IsWavedOff = false;
            identificationDto3.IsDeferred = false;
            identificationDto3.IdentificationTypeId = "18";
            identificationDto3.IdentificationDocTypeId = "18";
            identificationDto3.IdentificationDocId = "";

            ProfileIdentification.Add(identificationDto3);

            AgencyIdentificationDto identificationDto4 = new AgencyIdentificationDto();
            identificationDto4.Id = "";
            identificationDto4.IsWavedOff = false;
            identificationDto4.IsDeferred = false;
            identificationDto4.IdentificationTypeId = "19";
            identificationDto4.IdentificationDocTypeId = "34";
            identificationDto4.IdentificationDocId = "";

            ProfileIdentification.Add(identificationDto4);

            AgencyIdentificationDto identificationDto5 = new AgencyIdentificationDto();
            identificationDto5.Id = "";
            identificationDto5.IsWavedOff = false;
            identificationDto5.IsDeferred = false;
            identificationDto5.IdentificationTypeId = "20";
            identificationDto5.IdentificationDocTypeId = "35";
            identificationDto5.IdentificationDocId = "";

            ProfileIdentification.Add(identificationDto5);

            AgencyIdentificationDto identificationDto6 = new AgencyIdentificationDto();
            identificationDto6.Id = "";
            identificationDto6.IsWavedOff = false;
            identificationDto6.IsDeferred = false;
            identificationDto6.IdentificationTypeId = "21";
            identificationDto6.IdentificationDocTypeId = "36";
            identificationDto6.IdentificationDocId = "";

            ProfileIdentification.Add(identificationDto6);

            AgencyIdentificationDto identificationDto7 = new AgencyIdentificationDto();
            identificationDto7.Id = "";
            identificationDto7.IsWavedOff = false;
            identificationDto7.IsDeferred = false;
            identificationDto7.IdentificationTypeId = "22";
            identificationDto7.IdentificationDocTypeId = "37";
            identificationDto7.IdentificationDocId = "";

            ProfileIdentification.Add(identificationDto7);

            return ProfileIdentification;
        }

        private async Task ProcessStaffDataAsync(DataTable dataTable)
        {
            var duplicateMobileNumber = dataTable.AsEnumerable().GroupBy(a => a["MobileNumber"]).Where(gr => gr.Count() > 1).ToList();
            var duplicateEmailId = dataTable.AsEnumerable().GroupBy(a => a["EmailId"]).Where(gr => gr.Count() > 1).ToList();
            var duplicateEmployeeId = dataTable.AsEnumerable().GroupBy(a => a["EmployeeId"]).Where(gr => gr.Count() > 1).ToList();

            if (dataTable.Rows.Count == 0)
            {
                // Option 1: Add a row manually with failure reason
                DataRow newRow = dataTable.NewRow();
                newRow["Id"] = SequentialGuid.NewGuidString();
                newRow["IsFailed"] = "True";
                newRow["FailureReason"] = "The uploaded file contains no data.";
                dataTable.Rows.Add(newRow);

                // Optionally return or log here
                return; // or continue if you want to allow further execution
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                List<BulkUserErrorList> errorlist = new List<BulkUserErrorList>();
                AddCompanyUserDto addcompanyuserDto = await ValidateAndConstructStaffModelAsync(dataTable, i, errorlist);


                var mobilecheck = duplicateMobileNumber?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addcompanyuserDto.PrimaryMobileNumber).FirstOrDefault();

                if (mobilecheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 6;
                    error.errormessage = "Duplicate MobileNumber in File";
                    errorlist.Add(error);
                }

                var emailcheck = duplicateEmailId?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addcompanyuserDto.PrimaryEMail).FirstOrDefault();

                if (emailcheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 5;
                    error.errormessage = "Duplicate EmailId in File";
                    errorlist.Add(error);
                }

                var employeeidcheck = duplicateEmployeeId?.Where(a => (a.Key != null && a.Key.ToString() != "") && a.Key.ToString() == addcompanyuserDto.EmployeeID).FirstOrDefault();

                if (employeeidcheck != null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 2;
                    error.errormessage = "Duplicate EmployeeId in File";
                    errorlist.Add(error);
                }

                ValidateAddStaffUtility validateAddStaffUtility = new ValidateAddStaffUtility();
                var validateerrorlist = await validateAddStaffUtility.ValidateAddStaffAsync(addcompanyuserDto);

                foreach (var err in validateerrorlist)
                {
                    errorlist.Add(err);
                }

                if (errorlist.Count() > 0)
                {
                    string errormessage = "";

                    foreach (var error in errorlist.OrderBy(a => a.sequence))
                    {
                        errormessage = errormessage + " | " + error.errormessage;
                    }
                    dataTable.Rows[i]["Id"] = SequentialGuid.NewGuidString();
                    dataTable.Rows[i]["IsFailed"] = "True";
                    dataTable.Rows[i]["FailureReason"] = errormessage;
                }
                else
                {
                    addcompanyuserDto.PrimaryEMail = addcompanyuserDto.PrimaryEMail.ToLower();
                    AddCompanyUserCommand cmd = new AddCompanyUserCommand();
                    cmd.Dto = addcompanyuserDto;
                    await _bus.Send(cmd);

                    dataTable.Rows[i]["Id"] = SequentialGuid.NewGuidString();
                    dataTable.Rows[i]["IsFailed"] = "False";
                }
            }
        }

        private async Task<AddCompanyUserDto> ValidateAndConstructStaffModelAsync(DataTable dataTable, int i, List<BulkUserErrorList> errorlist)
        {
            AddCompanyUserDto addcompanyuserDto = new AddCompanyUserDto();
            addcompanyuserDto.Roles = new List<UserDesignationDto>();
            UserDesignationDto role = new UserDesignationDto();
            Department? department;
            Designation? designation;
            BaseBranch? branch;
            string departmentname = dataTable.Rows[i]["Department"].ToString();

            if (string.IsNullOrEmpty(departmentname))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 7;
                error.errormessage = "Department is Required";
                errorlist.Add(error);
            }
            else
            {
                department = await _repoFactory.GetRepo().FindAll<Department>().Where(a => a.Name == departmentname).FirstOrDefaultAsync();

                if (department == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 7;
                    error.errormessage = "Department is invalid";
                    errorlist.Add(error);
                }
                else
                {
                    role.DepartmentId = department?.Id;
                }
            }

            string designationname = dataTable.Rows[i]["Designation"].ToString();

            if (string.IsNullOrEmpty(designationname))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 8;
                error.errormessage = "Designation is Required";
                errorlist.Add(error);
            }
            else
            {
                designation = await _repoFactory.GetRepo().FindAll<Designation>().Where(a => a.Name == designationname).FirstOrDefaultAsync();

                if (designation == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 8;
                    error.errormessage = "Designation is invalid";
                    errorlist.Add(error);
                }
                else
                {
                    role.DesignationId = designation?.Id;
                }
            }

            string branchname = dataTable.Rows[i]["BranchName"].ToString();
            if (string.IsNullOrEmpty(branchname))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 9;
                error.errormessage = "BranchName is Required";
                errorlist.Add(error);
            }
            else
            {
                branch = await _repoFactory.GetRepo().FindAll<BaseBranch>().Where(a => a.FirstName == branchname).FirstOrDefaultAsync();

                if (branch == null)
                {
                    BulkUserErrorList error = new BulkUserErrorList();
                    error.sequence = 9;
                    error.errormessage = "BranchName is invalid";
                    errorlist.Add(error);
                }
                else
                {
                    addcompanyuserDto.BaseBranchId = branch?.Id;
                }
            }
            string userType = dataTable.Rows[i]["UserType"].ToString();
            if (string.IsNullOrEmpty(userType))
            {
                BulkUserErrorList error = new BulkUserErrorList();
                error.sequence = 1;
                error.errormessage = "User Type is Required";
                errorlist.Add(error);
            }
            else
            {
                bool isValidUserType = Enum.GetNames(typeof(UserTypeEnum))
                           .Any(e => e.Equals(userType, StringComparison.OrdinalIgnoreCase));

                if (!isValidUserType)
                {
                    errorlist.Add(new BulkUserErrorList
                    {
                        sequence = 1,
                        errormessage = $"Invalid User Type: '{userType}'. User Type must be one of: FOS, Telecaller, or Others."
                    });
                }
                else
                {
                    string userTypeNormalized = Enum.GetNames(typeof(UserTypeEnum)).FirstOrDefault(e => e.Equals(userType, StringComparison.OrdinalIgnoreCase));
                    var parsedUserType = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), userTypeNormalized);
                    addcompanyuserDto.UserType = parsedUserType.ToString();
                }

            }
            addcompanyuserDto.SetGeneratedId(_pkGenerator.GenerateKey());
            addcompanyuserDto.IsSaveAsDraft = true;
            addcompanyuserDto.DomainId = dataTable.Rows[i]["EmployeeId"].ToString();
            addcompanyuserDto.EmployeeID = dataTable.Rows[i]["EmployeeId"].ToString();
            addcompanyuserDto.FirstName = dataTable.Rows[i]["FirstName"].ToString();
            addcompanyuserDto.LastName = dataTable.Rows[i]["LastName"].ToString();
            addcompanyuserDto.PrimaryEMail = dataTable.Rows[i]["EmailId"].ToString();
            addcompanyuserDto.PrimaryMobileNumber = dataTable.Rows[i]["MobileNumber"].ToString();

            addcompanyuserDto.address = new AddressDto();
                        
            addcompanyuserDto.ProductScopes = new List<UserProductScopeDto>();
            addcompanyuserDto.GeoScopes = new List<UserGeoScopeDto>();
            addcompanyuserDto.BucketScopes = new List<UserBucketScopeDto>();
            addcompanyuserDto.PlaceOfWork = new List<CompanyUserPlaceOfWorkDto>();
            addcompanyuserDto.CreditAccountDetails = new CreditAccountDetailsDto();
            addcompanyuserDto.Languages = new List<LanguageDto>();

            addcompanyuserDto.Roles.Add(role);

            addcompanyuserDto.SetAppContext(_flexAppContext);
            return addcompanyuserDto;
        }

        public async Task<bool> ValidateHeadersAsync(DataTable dataTable, string type)
        {
            bool IsCorrectFileHeader = true;

            var dynamicHeaders = dataTable.Columns.Cast<DataColumn>()
                .Select(x => x.ColumnName?.Trim())
                .ToArray();

            List<string> staticHeaders = await _repoFactory.GetRepo().FindAll<CategoryItem>()
                                                .Where(x => string.Equals(x.CategoryMasterId, CategoryMasterEnum.UsersCreateHeaders.Value)
                                                         && string.Equals(x.Code, type))
                                                .OrderBy(x => x.Id)
                                                .Select(x => x.Name.Trim())
                                                .ToListAsync();

            // Use a case-insensitive comparison for both lists
            var result = staticHeaders.Where(x1 => !dynamicHeaders.Any(x2 => string.Equals(x1, x2)))
                         .Union(dynamicHeaders.Where(x1 => !staticHeaders.Any(x2 => string.Equals(x1, x2))));

            IsCorrectFileHeader = !result.Any();

            return IsCorrectFileHeader;
        }


        private BulkUserDateCheck IsDateValid(string date)
        {
            DateTime d;
            bool validate = false;

            BulkUserDateCheck bulkUserDateCheck = new BulkUserDateCheck();

            validate = DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
            bulkUserDateCheck.validate = validate;
            bulkUserDateCheck.Date = d;

            return bulkUserDateCheck;
        }
    }
}