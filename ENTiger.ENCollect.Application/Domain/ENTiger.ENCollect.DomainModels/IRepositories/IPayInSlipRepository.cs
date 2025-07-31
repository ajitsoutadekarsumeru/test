namespace ENTiger.ENCollect
{
    public interface IPayInSlipRepository
    {
        Task<PayInSlip?> GetPayInSlipByIdAsync(string id, FlexAppContextBridge context);

    }
}
