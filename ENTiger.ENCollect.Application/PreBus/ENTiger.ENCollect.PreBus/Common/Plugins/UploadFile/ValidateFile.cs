﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using ENTiger.ENCollect.DomainModels.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ENTiger.ENCollect.DomainModels;
using ENTiger.ENCollect;
using Microsoft.Extensions.Options;

namespace ENTiger.ENCollect.CommonModule.UploadFileCommonPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateFile : FlexiBusinessRuleBase, IFlexiBusinessRule<UploadFileDataPacket>
    {
        public override string Id { get; set; } = "3a135d1c4681bd8ee9e3d87457d68500";
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

        public virtual async Task Validate(UploadFileDataPacket packet)
        {
            IFormFile file = packet.Dto.file;
                        
            if (!_fileValidationUtility.ValidateUploadFile(file, out string errorMessage)) 
            {
                packet.AddError("Error", errorMessage);
            }
            string filename = Path.GetFileNameWithoutExtension(file.FileName);
            if (!Regex.IsMatch(filename, @"^\w+$"))
            {
                packet.AddError("Error", "Filename should not contain any special characters");
            }
            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
