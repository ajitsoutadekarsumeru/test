
namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetCustomerConsentByIdDto : DtoBridge
    {
        public string? ConsentId { get; set; }
        public string? AccountId { get; set; }
        public string? UserId { get; set; }
        public DateTime? VisitRequestedDateTime { get; set; }  //7pm - 7am
        public DateTime? ConsentSentDate { get; set; }  // created date
        public DateTime? ConsentStatusDate { get; set; }  // created date
        public DateTime? ExpiryDateTime { get; set; }   //cronjob to run @ 8am  every morning
        public bool? IsActive { get; set; }
        public string Status { get; set; }  // Enum for Pending, Accepted, Rejected, Expired
    }
}
