using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class CancelSettlementDto : DtoBridge
    {
        public List<string> Ids { get; set; }
        public string? WorkflowName { get; set; }
        public string? WorkflowInstanceId { get; set; }
        public string? StepName { get; set; }
        public string? StepType { get; set; }
    }
   

}
