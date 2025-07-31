using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISendPaymentLinkViaSMS : IAmFlexSubscriber<PaymentLinkGeneratedEvent>
    {
        
    }
}
