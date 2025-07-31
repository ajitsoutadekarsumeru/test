namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class UpdatePayInSlipDto : DtoBridge
    {
        public string Id { get; set; }
        public string? CMSPayInSlipNo { get; set; }
        public string? TRN { get; set; }
    }
}