﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AllocationModule.PrimaryAllocationByFilterAllocationPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidatePrimaryUnAllocation : FlexiBusinessRuleBase, IFlexiBusinessRule<PrimaryUnAllocationByBatchDataPacket>
    {
        public override string Id { get; set; } = "3a139de477983981879754d9befc1022";
        public override string FriendlyName { get; set; } = "ValidatePrimaryUnAllocation";
        protected readonly IFileSystem _fileSystem;
        protected readonly ILogger<ValidatePrimaryUnAllocation> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileValidationUtility _fileValidationUtility;
        public ValidatePrimaryUnAllocation(
            ILogger<ValidatePrimaryUnAllocation> logger, 
            IRepoFactory repoFactory, 
            IOptions<FilePathSettings> fileSettings, 
            IFileValidationUtility fileValidationUtility,
            IFileSystem fileSystem)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _fileSettings = fileSettings.Value;
            _fileValidationUtility = fileValidationUtility;
            _fileSystem = fileSystem;
        }

        public virtual async Task Validate(PrimaryUnAllocationByBatchDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);
            string _filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            if (!_fileValidationUtility.CheckIfFileExists(_filepath, packet.Dto.Name))
            {
                packet.AddError("Error", "Invalid File");
            }
            await Task.CompletedTask;
        }
    }
}
