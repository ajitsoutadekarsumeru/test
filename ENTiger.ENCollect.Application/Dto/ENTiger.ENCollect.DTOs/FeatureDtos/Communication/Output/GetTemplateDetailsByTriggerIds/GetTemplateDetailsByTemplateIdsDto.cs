using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTemplateDetailsByTemplateIdsDto : DtoBridge
    {
        public string Id { get; set; }
        public string TemplateType { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public bool IsAvailableInAccountDetails { get; set; }
    }
}
