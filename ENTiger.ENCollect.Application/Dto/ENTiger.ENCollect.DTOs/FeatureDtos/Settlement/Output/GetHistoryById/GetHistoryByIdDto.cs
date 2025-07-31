using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetHistoryByIdDto : DtoBridge
    {
        public List<WorkFlowHistoryDto> WorkflowHistory { get; set; }
    }

    public class WorkFlowHistoryDto
    {
        public DateTime StatusUpdatedDate { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        public string RejectionReason { get; set; }
        public string EmployeeId { get; set; }
    }
}
