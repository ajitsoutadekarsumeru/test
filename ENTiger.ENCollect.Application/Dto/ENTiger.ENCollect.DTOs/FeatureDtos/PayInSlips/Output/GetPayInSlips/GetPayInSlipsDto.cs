namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipsDto : DtoBridge
    {
        public string? payInSlipCode { get; set; }
        public string? CMSPayInSlipNo { get; set; }
        public string? Id { get; set; }
    }
}