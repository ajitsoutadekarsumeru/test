using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTriggerDetailsByTriggerIdsDto : DtoBridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TriggerTypeId { get; set; }
        public int? DaysOffset { get; set; } //Xth value
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
