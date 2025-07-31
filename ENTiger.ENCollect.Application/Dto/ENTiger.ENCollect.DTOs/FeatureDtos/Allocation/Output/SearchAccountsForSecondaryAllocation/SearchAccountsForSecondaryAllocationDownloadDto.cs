namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public class SearchAccountsForSecondaryAllocationDownloadDto : DtoBridge
    {
        public string? AccountNumber { get; set; }
        public string? CUSTOMERID { get; set; }
        public string? CUSTOMERNAME { get; set; }
        public string? BRANCH { get; set; }
        public string? Region { get; set; }
        public string? Zone { get; set; }
        public string? NPAFlag { get; set; }
        public string? AllocationOwnerName { get; set; }
        public string? TCallingAgencyName { get; set; }
        public string? TCallingAgentName { get; set; }
        public string? AgencyName { get; set; }
        public string? AgentName { get; set; }
        public string? AllocationDate { get; set; }
        public string? ProductGroup { get; set; }
        public string? PRODUCT { get; set; }
        public string? SubProduct { get; set; }
        public string? SCHEME_DESC { get; set; }
        public string? STATE { get; set; }
        public string? CITY { get; set; }
        public string? CURRENT_DPD { get; set; }
        public string? CURRENT_BUCKET { get; set; }
        public string? BUCKET { get; set; }
        public string? TOS { get; set; }
        public string? TOTAL_OUTSTANDING { get; set; }
        public string? TOTAL_ARREARS { get; set; }
        public string? CURRENT_POS { get; set; }
        public string? EMI_OD_AMT { get; set; }
        public string? INTEREST_OD { get; set; }
        public string? PRINCIPAL_OD { get; set; }
        public string? EMIAMT { get; set; }
        public string? OTHER_CHARGES { get; set; }
        public string? DueDate { get; set; }
        public string? PRIMARY_CARD_NUMBER { get; set; }
        public string? BILLING_CYCLE { get; set; }
        public string? LAST_STATEMENT_DATE { get; set; }
        public string? CURRENT_MINIMUM_AMOUNT_DUE { get; set; }
        public string? CURRENT_TOTAL_AMOUNT_DUE { get; set; }
        public string? RESIDENTIAL_CUSTOMER_CITY { get; set; }
        public string? RESIDENTIAL_CUSTOMER_STATE { get; set; }
        public string? RESIDENTIAL_PIN_CODE { get; set; }
        public string? RESIDENTIAL_COUNTRY { get; set; }
        public string? AllocationOwnerExpiryDate { get; set; }
        public string? TeleCallerAgencyAllocationExpiryDate { get; set; }
        public string? TeleCallerAllocationExpiryDate { get; set; }
        public string? AgencyAllocationExpiryDate { get; set; }
        public string? CollectorAllocationExpiryDate { get; set; }
        public string? AgentAllocationExpiryDate { get; set; }
        public string? LenderId { get; set; }
        public string? MAILINGMOBILE { get; set; }
        public string? MAILINGZIPCODE { get; set; }
        public string? OVERDUE_DAYS { get; set; }
    }
}