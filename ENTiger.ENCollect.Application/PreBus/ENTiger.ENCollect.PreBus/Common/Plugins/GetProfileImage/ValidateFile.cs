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

namespace ENTiger.ENCollect.CommonModule.GetProfileImageCommonPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateFile : FlexiBusinessRuleBase, IFlexiBusinessRule<GetProfileImageDataPacket>
    {
        public override string Id { get; set; } = "3a1362c79d29e70153b7e5058c6c8068";
        public override string FriendlyName { get; set; } = "ValidateFile";

        protected readonly ILogger<ValidateFile> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly IFileValidationUtility _fileValidationUtility;

        public ValidateFile(ILogger<ValidateFile> logger, IRepoFactory repoFactory, IFileValidationUtility fileValidationUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _fileValidationUtility = fileValidationUtility;
        }

        public virtual async Task Validate(GetProfileImageDataPacket packet)
        {
            if (!_fileValidationUtility.ValidateFileName(packet.Dto.FileName, out string errorMessage))
            {
                packet.AddError("Error", errorMessage);
            }
            if (!_fileValidationUtility.ValidateDownloadFile(packet.Dto.FileName, out string message))
            {
                packet.AddError("Error", message);
            }

            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
