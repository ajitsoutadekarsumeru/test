namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTodaysViewDto : DtoBridge
    {
        public BrokenTodaysView brokentodaysqueue { get; set; }
        public TodaysPTPView todaysptpqueue { get; set; }
        public RescheduledTodaysView rescheduled { get; set; }
        public PTPConfirmedView PTPConfirmedToday { get; set; }
        public PaidTodaysView PaidTodaysQueue { get; set; }
        public contactsUniqueTodaysView contactsUniqueTodaysQueue { get; set; }
        public ConnectsUniqueTodaysView ConnectsUniqueTodaysQueue { get; set; }
        public WorkableAccountsTodaysView WorkableAccounts { get; set; }
    }

    public class BrokenTodaysView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }

        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class TodaysPTPView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }

        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class RescheduledTodaysView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }

        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class PTPConfirmedView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }

        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class PaidTodaysView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }

        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class contactsUniqueTodaysView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }

        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class ConnectsUniqueTodaysView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }

        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class WorkableAccountsTodaysView
    {
        public Int64 count { get; set; }
        public decimal? pos { get; set; }
        public ICollection<TodaysViewAccountSearchDto> accountdetails { get; set; }
    }

    public class TodaysViewAccountSearchDto
    {
        public string? Id { get; set; }
        public string? CUSTOMERNAME { get; set; }
        public string? accountno { get; set; }
        public string? BOMBuket { get; set; }
        public string? Location { get; set; }
        public DateTime? cycledate { get; set; }

        //public long? CURRENT_BUCKET { get; set; }
        public string? CURRENT_BUCKET { get; set; }

        public string? EMIAMT { get; set; }
        public string? PHONE1 { get; set; }
        public string? PHONE2 { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public string? EMI_OD_AMT { get; set; }
        public decimal? CLEAR_AMOUNT { get; set; }
        public string? LatePaymentcharges { get; set; }
        public string? Othercharges { get; set; }
        public string? CBC { get; set; }
        public string? CustomerID { get; set; }
        public string? EMIAmount { get; set; }
        public string? Product { get; set; }
        public string? MonthOpeningPOS { get; set; }
        public string? agencyCode { get; set; }
        public string? CollectorName { get; set; }
        public string? TeleCallerName { get; set; }
        public string? TeleCallingAgencyCode { get; set; }
    }

    public class TodaysViewAccountDetailDto
    {
        public string? Id { get; set; }
        public string? DispCode { get; set; }
        public string? CURRENT_POS { get; set; }
        public string? CUSTOMERNAME { get; set; }
        public string? CUSTOMERID { get; set; }

        // public DateTime? cycledate { get; set; }
        //public long? CURRENT_BUCKET { get; set; }
        public string? AGREEMENTID { get; set; }

        public string? EMIAMT { get; set; }
        public string? CURRENT_BUCKET { get; set; }
        public string? PRODUCT { get; set; }
        public decimal? BOM_POS { get; set; }
        public string? CustomId { get; set; }
        public DateTime? LatestFeedbackDate { get; set; }
        public DateTime? LatestPTPDate { get; set; }
        public DateTime? LatestPaymentDate { get; set; }
        public string? LatestPTPAmount { get; set; }
        public string? LatestFeedbackId { get; set; }
        public string? GroupName { get; set; }
        public string? EMI_OD_AMT { get; set; }
        public string? PENAL_PENDING { get; set; }
        public string? Agency { get; set; }
        public string? AgencyId { get; set; }
        public string? Collector { get; set; }
        public string? CollectorId { get; set; }
        public string? TeleCaller { get; set; }
        public string? TeleCallerId { get; set; }
        public string? TeleCallingAgency { get; set; }
        public string? TeleCallingAgencyId { get; set; }
    }
}