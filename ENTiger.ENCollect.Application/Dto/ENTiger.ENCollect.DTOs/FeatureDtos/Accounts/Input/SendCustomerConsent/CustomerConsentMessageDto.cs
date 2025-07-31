
namespace ENTiger.ENCollect.AccountsModule
{
    public partial class CustomerConsentMessageDto : DtoBridge
    {
        public DateTime? Date { get; set; }
        public string? Link { get; set; }
        public string? ClientName { get; set; }
    }
}
