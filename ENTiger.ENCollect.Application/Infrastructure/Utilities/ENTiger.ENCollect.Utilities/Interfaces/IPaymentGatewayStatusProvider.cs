namespace ENTiger.ENCollect
{
    public interface IPaymentGatewayStatusProvider
    {
        Task<bool> FetchPaymentStatusAsync(NotificationRequest request);
    }
}
