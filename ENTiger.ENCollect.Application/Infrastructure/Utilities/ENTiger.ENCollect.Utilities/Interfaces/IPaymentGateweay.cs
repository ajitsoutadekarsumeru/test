namespace ENTiger.ENCollect
{
    public interface IPaymentGateway
    {
        Task<PaymentTransactionDto> GetPaymentLinkDetails(CollectionDtoWithId collection, List<FeatureMasterDtoWithId> paymentDetails, string tenantId);

        Task SendPaymentLinkEmail(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId paymentTransactionDto, string tenantId);

        Task SendPaymentLinkSMS(CollectionDtoWithId collectionDto, PaymentTransactionDtoWithId paymentTransactionDto, string tenantId);
    }
}