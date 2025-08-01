﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommonModule.GetLogoCommonPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetLogo : FlexiBusinessRuleBase, IFlexiBusinessRule<GetLogoDataPacket>
    {
        public override string Id { get; set; } = "3a139ac87deddbac74fd1d06aaeadd77";
        public override string FriendlyName { get; set; } = "GetLogo";

        protected readonly ILogger<GetLogo> _logger;
        protected readonly IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public GetLogo(ILogger<GetLogo> logger, IFlexTenantRepository<FlexTenantBridge> repoTenantFactory)
        {
            _logger = logger;
            _repoTenantFactory = repoTenantFactory;
        }

        public virtual async Task Validate(GetLogoDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            string tenantId = _flexAppContext.TenantId;

            packet.Logo = await _repoTenantFactory.FindAll<FlexTenantBridge>().Where(x => x.Id == tenantId).Select(x => x.Logo).FirstOrDefaultAsync();
        }
    }
}
