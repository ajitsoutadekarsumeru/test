using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTriggerByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public bool? IsDeleted { get; set; }
        public string Name { get; set; }
        public string TriggerTypeId { get; set; }
        public int? DaysOffset { get; set; } //Xth value
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string RecipientType { get; set; }
        public List<string> ConnectedTemplates { get; set; }

    }
}
