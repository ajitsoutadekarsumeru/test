using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SettlementModule
{
    
    public partial class ProcessSettlementNegotiateDto : DtoBridge 
    {
        [StringLength(32)]
        public string Id { get; set; }
        public string Status { get; set; }
       
        public string WorkflowName { get; set; }
        public string WorkflowInstanceId { get; set; }
        public string StepName { get; set; }
    }

}
