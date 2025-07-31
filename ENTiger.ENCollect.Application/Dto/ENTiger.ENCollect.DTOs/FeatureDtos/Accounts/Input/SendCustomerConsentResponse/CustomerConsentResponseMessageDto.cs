
namespace ENTiger.ENCollect.AccountsModule
{
    public partial class CustomerConsentResponseMessageDto : DtoBridge
    {
        public string? AccountNo { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }
        public string? ClientName { get; set; }
        public string? UserName { get; set; }
    }
}
