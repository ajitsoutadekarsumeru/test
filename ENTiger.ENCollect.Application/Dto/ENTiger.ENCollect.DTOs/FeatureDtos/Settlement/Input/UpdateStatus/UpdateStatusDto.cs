using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class UpdateStatusDto : DtoBridge
    {
        public List<UpdateStatusOfSettlementDto> Updates { get; set; }
    }
    public partial class UpdateStatusOfSettlementDto : DtoBridge 
    {
        [StringLength(32)]
        public string Id { get; set; }
        public string Status { get; set; }
        public string? Remarks { get; set; }
        public string? RejectionReason { get; set; }
        public decimal? RenegotiateAmount { get; set; }

        public string? CustomerSignedLetter { get; set; }

        public string WorkflowName { get; set; }
        public string WorkflowInstanceId { get; set; }
        public string StepName { get; set; }
        public string StepType { get; set; }
    }

}
