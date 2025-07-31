using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public class UpdateCollectionStatusOnPayInSlipAcknowledgedEvent : FlexEventBridge<FlexAppContextBridge>
    {

        public string? PayInSlipId { get; set; }
    }


}
