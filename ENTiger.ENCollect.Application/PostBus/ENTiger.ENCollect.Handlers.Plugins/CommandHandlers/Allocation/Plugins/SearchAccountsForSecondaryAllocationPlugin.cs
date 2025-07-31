using System.IO.Abstractions;
using System.IO.Compression;
using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SearchAccountsForSecondaryAllocationPlugin : FlexiPluginBase, IFlexiPlugin<SearchAccountsForSecondaryAllocationPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1395c162d46f442393b1d233abde32";
        public override string FriendlyName { get; set; } = "SearchAccountsForSecondaryAllocationPlugin";

        protected string EventCondition = "";
        protected readonly ILogger<SearchAccountsForSecondaryAllocationPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected AllocationDownload? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ICustomUtility _customUtility;
        private string userId = string.Empty;

        private readonly ICsvExcelUtility _csvExcelUtility;

        private readonly FilePathSettings _fileSettings;

        private readonly IFileSystem _fileSystem;

        protected AuditEventData _auditData;
        public SearchAccountsForSecondaryAllocationPlugin(ILogger<SearchAccountsForSecondaryAllocationPlugin> logger, IFlexHost flexHost
                ,IRepoFactory repoFactory, ICsvExcelUtility csvExcelUtility, IOptions<FilePathSettings> fileSettings
                ,ICustomUtility customUtility, IFileSystem fileSystem)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _csvExcelUtility = csvExcelUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _customUtility = customUtility;
        }

        public virtual async Task Execute(SearchAccountsForSecondaryAllocationPostBusDataPacket packet)
        {
            string destPath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            var inputmodel = packet.Cmd.Dto;
            userId = _flexAppContext.UserId;

            _model = _flexHost.GetDomainModel<AllocationDownload>().SearchAccountsForSecondaryAllocation(packet.Cmd);

            string customid = inputmodel.customId;
            _model.Status = FileStatusEnum.Processing.Value.ToLower();
            _model.AllocationType = AllocationTypeEnum.Secondary.Value.ToLower();
            string jsonStr = JsonConvert.SerializeObject(inputmodel);
            _model.InputJson = jsonStr.ToString();

            ApplicationUser? user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.Id == userId).FirstOrDefaultAsync();

            Accountability? accountability = await _repoFactory.GetRepo().FindAll<Accountability>().Where(x => x.ResponsibleId == userId).FirstOrDefaultAsync();

            var accounts = await _repoFactory.GetRepo().FindAll<LoanAccount>().byProductGroup(inputmodel.ProductGroup)
                                 .IsLoanAccount(inputmodel.isloanaccount)
                                 .byAccountUploadedDate(inputmodel.FromDate, inputmodel.ToDate)
                                 .byproduct(inputmodel.Product)
                                 .bysubProduct(inputmodel.SubProduct)
                                 .bydownloadBucket(inputmodel.Bucket)
                                 .ByAccountAgencyId(inputmodel.AgencyId)
                                 .ByAccountTeleCallingAgencyId(inputmodel.TeleCallingAgencyId)
                                 .byZone(inputmodel.Zone)
                                 .byRegion(inputmodel.Region)
                                 .bycity(inputmodel.City)
                                 .byBranch(inputmodel.Branch)
                                 .BySecondAllocation(inputmodel.IsAllocated, inputmodel.IsUnAllocated, user, accountability, accountability.CommisionerId, userId)
                                 .Select(a => new
                                 {
                                     AccountNumber = a.AGREEMENTID ?? string.Empty,
                                     CUSTOMERID = a.CUSTOMERID ?? string.Empty,
                                     CUSTOMERNAME = a.CUSTOMERNAME ?? string.Empty,
                                     ProductGroup = a.ProductGroup ?? string.Empty,
                                     PRODUCT = a.PRODUCT ?? string.Empty,
                                     SubProduct = a.SubProduct ?? string.Empty,
                                     SCHEME_DESC = a.SCHEME_DESC ?? string.Empty,
                                     Zone = a.ZONE ?? string.Empty,
                                     Region = a.Region ?? string.Empty,
                                     STATE = a.STATE ?? string.Empty,
                                     CITY = a.CITY ?? string.Empty,
                                     BRANCH = a.BRANCH ?? string.Empty,
                                     CURRENT_DPD = Convert.ToString(a.CURRENT_DPD) ?? string.Empty,
                                     CURRENT_BUCKET = a.CURRENT_BUCKET ?? string.Empty,
                                     BUCKET = Convert.ToString(a.BUCKET) ?? string.Empty,
                                     OVERDUE_DAYS = a.OVERDUE_DAYS ?? string.Empty,
                                     TOS = a.TOS ?? string.Empty,
                                     TOTAL_OUTSTANDING = Convert.ToString(a.TOTAL_OUTSTANDING) ?? string.Empty,
                                     TOTAL_ARREARS = a.TOTAL_ARREARS ?? string.Empty,
                                     CURRENT_POS = Convert.ToString(a.CURRENT_POS) ?? string.Empty,
                                     EMI_OD_AMT = Convert.ToString(a.EMI_OD_AMT) ?? string.Empty,
                                     INTEREST_OD = Convert.ToString(a.INTEREST_OD) ?? string.Empty,
                                     PRINCIPAL_OD = Convert.ToString(a.PRINCIPAL_OD) ?? string.Empty,
                                     EMIAMT = Convert.ToString(a.EMIAMT) ?? string.Empty,
                                     OTHER_CHARGES = a.OTHER_CHARGES ?? string.Empty,
                                     DueDate = a.DueDate ?? string.Empty,
                                     NPAFlag = a.NPA_STAGEID ?? string.Empty,
                                     PRIMARY_CARD_NUMBER = a.PRIMARY_CARD_NUMBER ?? string.Empty,
                                     BILLING_CYCLE = a.BILLING_CYCLE ?? string.Empty,
                                     LAST_STATEMENT_DATE = Convert.ToString(a.LAST_STATEMENT_DATE) ?? string.Empty,
                                     CURRENT_MINIMUM_AMOUNT_DUE = Convert.ToString(a.CURRENT_MINIMUM_AMOUNT_DUE) ?? string.Empty,
                                     CURRENT_TOTAL_AMOUNT_DUE = Convert.ToString(a.CURRENT_TOTAL_AMOUNT_DUE) ?? string.Empty,
                                     RESIDENTIAL_CUSTOMER_CITY = a.RESIDENTIAL_CUSTOMER_CITY ?? string.Empty,
                                     RESIDENTIAL_CUSTOMER_STATE = a.RESIDENTIAL_CUSTOMER_STATE ?? string.Empty,
                                     RESIDENTIAL_PIN_CODE = a.RESIDENTIAL_PIN_CODE ?? string.Empty,
                                     RESIDENTIAL_COUNTRY = a.RESIDENTIAL_COUNTRY ?? string.Empty,
                                     AllocationOwnerName = a.AllocationOwner.FirstName ?? string.Empty,
                                     AllocationOwnerExpiryDate = Convert.ToString(a.AllocationOwnerExpiryDate) ?? string.Empty,
                                     TCallingAgencyName = a.TeleCallingAgency.FirstName ?? string.Empty,
                                     TeleCallerAgencyAllocationExpiryDate = Convert.ToString(a.TeleCallerAgencyAllocationExpiryDate) ?? string.Empty,
                                     TCallingAgentName = a.TeleCaller.FirstName ?? string.Empty,
                                     TeleCallerAllocationExpiryDate = Convert.ToString(a.TeleCallerAllocationExpiryDate) ?? string.Empty,
                                     AgencyName = a.Agency.FirstName ?? string.Empty,
                                     AgencyAllocationExpiryDate = Convert.ToString(a.AgencyAllocationExpiryDate) ?? string.Empty,
                                     AgentName = a.Collector.FirstName ?? string.Empty,
                                     CollectorAllocationExpiryDate = Convert.ToString(a.CollectorAllocationExpiryDate) ?? string.Empty,
                                     AgentAllocationExpiryDate = Convert.ToString(a.AgentAllocationExpiryDate) ?? string.Empty,
                                     LenderId = a.LenderId ?? string.Empty,
                                     MAILINGMOBILE = a.MAILINGMOBILE ?? string.Empty,
                                     MAILINGZIPCODE = a.MAILINGZIPCODE ?? string.Empty,
                                     AllocationDate = Convert.ToString(a.LastUploadedDate) ?? string.Empty,
                                     AccountJSON = a.AccountJSON != null ? JsonConvert.DeserializeObject<Dictionary<string, string>>(a.AccountJSON.AccountJSON) : null
                                 }).ToListAsync();

            if (accounts != null)
            {
                var AccountNoList = _customUtility.FormatAccountsData(accounts.ToList(), _logger);
                _logger.LogInformation("SedondaryDownloadCommandHandler : Accounts Count - " + AccountNoList.Count);
                string fileName = "SecondaryAllocation_" + customid;

                string csvContent = _csvExcelUtility.ConvertListToCsvString(AccountNoList, true);
                string zipFilePath = await _csvExcelUtility.GenerateZipInMemoryAsync(fileName, destPath, csvContent);
                if (File.Exists(zipFilePath))
                {
                    _model.FileName = fileName + ".zip";
                    _model.CustomId = customid;
                    _model.Status = "Success";
                    _model.FilePath = zipFilePath;
                }
            }
            else
            {
                _model.Status = "Failed - No records";
                _model.CustomId = customid;
            }
            _model.SetAdded();
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(AllocationDownload).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(AllocationDownload).Name, _model.Id);
            }
        }
        public async Task<string> GenerateZipInMemoryAsync(string fileName, string destPath, 
             string csvContent)
        {
            //string fileName = $"SecondaryAllocation_{customId}";
            string zipFilePath = Path.Combine(destPath, $"{fileName}.zip");

            Directory.CreateDirectory(destPath);

            if (File.Exists(zipFilePath))
                File.Delete(zipFilePath);

            using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create, leaveOpen: false))
            {
                var entry = archive.CreateEntry($"{fileName}.csv");

                await using (var entryStream = entry.Open())
                await using (var writer = new StreamWriter(entryStream))
                {                    
                    await writer.WriteAsync(csvContent);
                }
            }

            return zipFilePath;
        }

        private async Task GenerateAndSendAuditEventAsync(SearchAccountsForSecondaryAllocationPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.SecondaryBulkAllocation.Value,
                Operation: AuditOperationEnum.Download.Value,
                JsonPatch: jsonPatch,
                InitiatorId: _flexAppContext?.UserId,
                TenantId: _flexAppContext?.TenantId,
                ClientIP: _flexAppContext?.ClientIP
            );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}