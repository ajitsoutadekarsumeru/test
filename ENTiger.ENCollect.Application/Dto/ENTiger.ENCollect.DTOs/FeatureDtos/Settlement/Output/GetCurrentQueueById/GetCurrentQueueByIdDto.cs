using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetCurrentQueueByIdDto : DtoBridge
    {
        public string EmployeeId { get; set; }
        public DateTime AssignedAt { get; set; }

    }
}
