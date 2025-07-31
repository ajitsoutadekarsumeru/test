using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SearchTriggersDto : DtoBridge
    {
        public string Id { get; set; }
        public string TriggerTypeId { get; set; }
        public string TriggerName { get; set; }
        public string TriggerTypeName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string Status { get; set; }
        public List<string> ConnectedTemplates { get; set; }
    }
   
}
