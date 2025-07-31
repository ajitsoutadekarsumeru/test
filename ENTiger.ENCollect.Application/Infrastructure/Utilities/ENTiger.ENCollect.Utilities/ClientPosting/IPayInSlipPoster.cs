namespace ENTiger.ENCollect
{
    public interface IPayInSlipPoster
    {
        Task PostPayInSlipAsync(PayInSlipDtoWithId payinSlip);

    }
}
