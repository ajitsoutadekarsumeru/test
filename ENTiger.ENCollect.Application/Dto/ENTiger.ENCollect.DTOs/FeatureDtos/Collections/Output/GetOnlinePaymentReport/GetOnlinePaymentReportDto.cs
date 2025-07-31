namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetOnlinePaymentReportDto : DtoBridge
    {
        //OnlinePaymentReportOutputApiModel
        public string AgencyId { get; set; }

        public string AgencyName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentName { get; set; }
        public string AgentId { get; set; }
        public string AccountNo { get; set; }
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public string CURRENT_BUCKET { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceiptDate { get; set; }
        public string TotalReceiptAmount { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionReferenceNo { get; set; }
        public string BankReferenceNo { get; set; }
        public string TransactionInitiatedDate { get; set; }
        public string TokenNo { get; set; }
        public string PaymentStatus { get; set; }
        public string TimeStamp { get; set; }
        public string ErrorMessage { get; set; }
        public string SendPaymentLinkType { get; set; }
    }
}