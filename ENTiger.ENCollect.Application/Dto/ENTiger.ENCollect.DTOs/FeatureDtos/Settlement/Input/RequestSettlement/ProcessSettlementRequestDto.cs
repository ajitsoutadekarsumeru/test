using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementRequestDto : DtoBridge
    {
        public string WorkflowInstanceId { get; set; } = string.Empty;
        public string DomainId { get; set; }
       
        public string Status { get; set; }

        public string WorkflowName { get; set; }
       
        public string StepName { get; set; }

    }



}
