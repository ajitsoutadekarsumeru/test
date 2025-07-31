using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetMyAccountsForOfflineDto : DtoBridge
    {
        public string AccountNo { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string CurrentBucket { get; set; }
        public string CurrentDPD { get; set; }
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string EMailId { get; set; }

        public decimal? EMIAmount { get; set; }
        public string BCC_PENDING { get; set; }
        public string Id { get; set; }
        public string MobileNo { get; set; }

        //public int MonthStartingBucket { get; set; }
        public string MonthStartingBucket { get; set; }

        public decimal? POS { get; set; }
        public string ProductName { get; set; }
        public decimal? PTPAmount { get; set; }
        public DateTime? PTPDate { get; set; }
        public string State { get; set; }
        public string TOS { get; set; }
        public decimal? PRINCIPLE_OVERDUE { get; set; }
        public decimal? INTEREST_OVERDUE { get; set; }
        public decimal? PENAL_PENDING { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public decimal? EMI_OD_AMT { get; set; }
        public decimal? LatestPTPAmount { get; set; }
        public string CentreID { get; set; }
        public string GroupID { get; set; }
        public string CentreName { get; set; }
        public string GroupName { get; set; }
    }
}