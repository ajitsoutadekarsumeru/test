﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AgencyUsersModule.UpdateAgentAgencyUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckPincodeBeforeUpdate : FlexiBusinessRuleBase, IFlexiBusinessRule<UpdateAgentDataPacket>
    {
        public override string Id { get; set; } = "3a12e7a13f0486d923e28aa1d1ba302d";
        public override string FriendlyName { get; set; } = "CheckPincodeBeforeUpdate";

        protected readonly ILogger<CheckPincodeBeforeUpdate> _logger;
        protected readonly IRepoFactory _repoFactory;
        bool ValidatePinCode = Convert.ToBoolean(AppConfigManager.AppSettings["ValidatePinCode"] ?? "false");
        protected FlexAppContextBridge? _flexAppContext;

        public CheckPincodeBeforeUpdate(ILogger<CheckPincodeBeforeUpdate> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(UpdateAgentDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);
            if (!packet.HasError)
            {
                if (ValidatePinCode)
                {
                    if (packet.Dto?.PlaceOfWork?.Count > 20)
                    {
                        packet.AddError("Errors", "Maxlength should not exceed 20 pincodes");
                    }
                }
            }

            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }

    }
}
