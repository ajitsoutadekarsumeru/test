namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class ChequeDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? InstrumentNo { get; set; }
        public string? InstrumentDate { get; set; }
        public string? MICRCode { get; set; }
        public string? IFSCCode { get; set; }
    }
}