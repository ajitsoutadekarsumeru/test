﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using ENTiger.ENCollect.DomainModels.Reports;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule.GetFileAllocationPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckFileAccess : FlexiBusinessRuleBase, IFlexiBusinessRule<GetFileDataPacket>
    {
        public override string Id { get; set; } = "3a1816dc3d48c6088f4e735b9197780d";
        public override string FriendlyName { get; set; } = "CheckFileAccess";

        protected readonly ILogger<CheckFileAccess> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly FileConfigurationSettings _fileConfigurationSettings;
        public CheckFileAccess(ILogger<CheckFileAccess> logger, IRepoFactory repoFactory, IOptions<FileConfigurationSettings> fileConfigurationSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _fileConfigurationSettings = fileConfigurationSettings.Value;
        }

        public virtual async Task Validate(GetFileDataPacket packet)
        {
            string FileExtension = _fileConfigurationSettings.DefaultExtension;
            if (!packet.HasError)
            {
                _repoFactory.Init(packet.Dto);
                _flexAppContext = packet.Dto.GetAppContext();  //do not remove this line

                string userId = _flexAppContext.UserId;
                List<string> filesList = new List<string>();

                var insightfiles = _repoFactory.GetRepo().FindAll<InsightDownloadFile>()
                                    .Where(x => x.CreatedBy == userId &&
                                    (x.FileName == packet.Dto.FileName)).ToList();

                if (insightfiles != null && insightfiles.Count() > 0)
                {
                    filesList.AddRange(insightfiles.Select(x => x.FileName).ToList());
                }
                
                if (filesList.Where(x => x != null && x.Contains(packet.Dto.FileName)).Count() == 0)
                {
                    _logger.LogError("CheckFileAccess : Access Denied");
                    packet.AddError("Error", "Access Denied");
                }
            }
            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
