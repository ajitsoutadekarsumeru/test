using ENTiger.ENCollect.CollectionsModule;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ImportCollectionsPlugin : FlexiPluginBase, IFlexiPlugin<ImportCollectionsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1852e2be5a3b92b876cfe39771404b";
        public override string FriendlyName { get; set; } = "ImportCollectionsPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<ImportCollectionsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        private readonly ICsvExcelUtility _csvExcelUtility;

        protected CollectionUploadFile? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public ImportCollectionsPlugin(ILogger<ImportCollectionsPlugin> logger, 
            IFlexHost flexHost, IRepoFactory repoFactory,
            IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem,
            ICsvExcelUtility csvExcelUtility)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
            _csvExcelUtility = csvExcelUtility;
        }

        public virtual async Task Execute(ImportCollectionsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);

            string fileName = await ExportDataToCsvAsync(packet);

            //Construct and Save the File related data inside CollectionUploadFile
            CollectionBulkUploadDto dto = new CollectionBulkUploadDto()
            {
                CustomId = DateTime.Now.ToString("MMddyyyyhhmmssfff"),
                FileName = fileName
            };
            dto.SetAppContext(_flexAppContext);
            CollectionBulkUploadCommand cmd = new CollectionBulkUploadCommand
            {
                Dto = dto
            };

            _model = _flexHost.GetDomainModel<CollectionUploadFile>().CollectionBulkUpload(cmd);


            //Save
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(CollectionUploadFile).Name, _model.Id);
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(CollectionUploadFile).Name, _model.Id);
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<string> ExportDataToCsvAsync(ImportCollectionsPostBusDataPacket packet)
        {
            string fileName = UploadTypeEnum.CollectionImport.Value + "_" +
                                     DateTime.Now.ToString("yyyyMMddhhmmssfff")
                                     + FileTypeEnum.CSV.Value;
            string filePath = _fileSystem.Path.Combine(_fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath), fileName);


            //Save input into CSV
            await _csvExcelUtility.ExportDataToCsvGenericAsync(packet.Cmd.Dto.CollectionList, filePath, false);
            return fileName;
        }


    }
}