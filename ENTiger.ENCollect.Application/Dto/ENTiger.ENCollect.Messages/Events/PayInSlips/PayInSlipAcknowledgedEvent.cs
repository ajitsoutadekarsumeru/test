using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public class PayInSlipAcknowledgedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string>? PayInSlipIds { get; set; }

        public string? PayInSlipId { get; set; }
    }


}
