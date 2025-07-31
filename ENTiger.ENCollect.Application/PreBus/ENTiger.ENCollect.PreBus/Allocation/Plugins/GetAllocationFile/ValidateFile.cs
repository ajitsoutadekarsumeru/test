using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AllocationModule.GetAllocationFileAllocationPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateFile : FlexiBusinessRuleBase, IFlexiBusinessRule<GetAllocationFileDataPacket>
    {
        public override string Id { get; set; } = "3a1816dc4210923b018f17c7de3157e4";
        public override string FriendlyName { get; set; } = "ValidateFile";

        protected readonly ILogger<ValidateFile> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly IFileValidationUtility _fileValidationUtility;
        private readonly IFileSystem _fileSystem;
        private readonly FilePathSettings _filePathSettings;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        public ValidateFile(ILogger<ValidateFile> logger, IRepoFactory repoFactory, IFileValidationUtility fileValidationUtility, IFileSystem fileSystem, IOptions<FilePathSettings> filePathSettings, IOptions<FileConfigurationSettings> fileConfigurationSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _fileValidationUtility = fileValidationUtility;
            _fileSystem = fileSystem;
            _filePathSettings = filePathSettings.Value;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
        }

        public virtual async Task Validate(GetAllocationFileDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            var primaryAllocationFile = await _repoFactory.GetRepo().FindAll<PrimaryAllocationFile>()
                                                    .FirstOrDefaultAsync(x => x.CustomId == packet.Dto.TransactionId);

            if (primaryAllocationFile == null)
            {
                packet.AddError("Error", "Invalid transaction id");
            }
            else
            {
                var sourceFilename = packet.Dto.FileName.Trim('"');
                string fileExtension = _fileSystem.Path.GetExtension(sourceFilename).ToLower();
                if (fileExtension != FileTypeEnum.CSV.Value && fileExtension != FileTypeEnum.XLSX.Value && fileExtension != FileTypeEnum.XLS.Value)
                {
                    packet.AddError("Error", "Invalid File");
                }
                else
                {
                    if (!_fileValidationUtility.ValidateFileName(sourceFilename, out string errorMessage))
                    {
                        packet.AddError("Error", errorMessage);
                    }
                    else
                    {
                        string defaultFileExtension = _fileConfigurationSettings.DefaultExtension;
                        string defaultFilePath = _fileSystem.Path.Combine(_filePathSettings.BasePath, _filePathSettings.IncomingPath);

                        string filePath = defaultFileExtension != fileExtension ?
                                            primaryAllocationFile.FilePath : 
                                            _fileSystem.Path.Combine(defaultFilePath, _filePathSettings.AllocationProcessedFilePath);

                        if (!_fileValidationUtility.ValidateDownloadFile(sourceFilename, out string message, filePath))
                        {
                            packet.AddError("Error", message);
                        }
                        else
                        {
                            packet.FilePath = filePath;
                            packet.Dto.FileName = sourceFilename;
                        }
                    }
                }
            }
        }
    }
}
