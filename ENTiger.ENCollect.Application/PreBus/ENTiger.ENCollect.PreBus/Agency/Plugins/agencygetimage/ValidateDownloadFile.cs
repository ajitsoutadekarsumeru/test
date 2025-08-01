﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Threading.Tasks;
using ENTiger.ENCollect.DomainModels;
using Microsoft.Extensions.Configuration;
using ENTiger.ENCollect;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AgencyModule.agencygetimageAgencyPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateDownloadFile : FlexiBusinessRuleBase, IFlexiBusinessRule<agencygetimageDataPacket>
    {
        public override string Id { get; set; } = "3a133ceb3709a406956a676cda8f836b";
        public override string FriendlyName { get; set; } = "ValidateDownloadFile";

        protected readonly ILogger<ValidateDownloadFile> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly IFileValidationUtility _fileValidationUtility;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        public ValidateDownloadFile(
            ILogger<ValidateDownloadFile> logger, 
            IRepoFactory repoFactory, 
            IFileValidationUtility fileValidationUtility, 
            IOptions<FilePathSettings> fileSettings, 
            IFileSystem fileSystem)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _fileValidationUtility = fileValidationUtility;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Validate(agencygetimageDataPacket packet)
        {
            if (!packet.HasError)
            { 
                string filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);

                if (!_fileValidationUtility.ValidateDownloadFile(packet.Dto.FileName, out string errorMessage, filepath))
                {
                    packet.AddError("Error", errorMessage);
                }
            }
            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
