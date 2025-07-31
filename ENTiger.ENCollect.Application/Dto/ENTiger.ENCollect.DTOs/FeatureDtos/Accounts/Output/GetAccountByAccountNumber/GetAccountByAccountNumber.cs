using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAccountByAccountNumberDto : DtoBridge
    {
        public string? AccountNo { get; set; }
        public string? Address { get; set; }
        public string? PropensityToPay { get; set; }
        public string? PropensityToPayConfidence { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? ContractId { get; set; }
        public string? CurrentBucket { get; set; }
        public string? CurrentDPD { get; set; }
        public string? DPD { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerId { get; set; }

        [StringLength(100)]
        public string? EMailId { get; set; }

        public decimal? EMIAmount { get; set; }
        public string? DMConsumerId { get; set; }
        public string? DMAccountId { get; set; }
        public string? BCC_PENDING { get; set; }
        public string? Id { get; set; }
        public string? MobileNo { get; set; }

        //public int MonthStartingBucket { get; set; }
        public string? MonthStartingBucket { get; set; }

        public decimal? POS { get; set; }
        public string? SchemeCode { get; set; }
        public string? ProductGroup { get; set; }
        public string? ProductName { get; set; }
        public string? SubProductName { get; set; }
        public decimal? PTPAmount { get; set; }
        public DateTime? PTPDate { get; set; }
        public string? State { get; set; }
        public decimal TOS { get; set; }
        public string? DepositAccountNumber { get; set; }
        public string? DepositBankName { get; set; }
        public string? LegalAndSettlementStatus { get; set; }
        public string? DepositBankBranchId { get; set; }
        public bool? IsPoolAccount { get; set; }
        public bool? IsDepositAccount { get; set; }
        public decimal? PRINCIPLE_OVERDUE { get; set; }
        public decimal? INTEREST_OVERDUE { get; set; }
        public decimal? CHARGE_OVERDUE { get; set; }
        public string? TOTAL_OVERDUE_AMT { get; set; }
        public decimal? PENAL_PENDING { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public decimal? EMI_OD_AMT { get; set; }
        public decimal? LatestPTPAmount { get; set; }
        public string? ProductGroupName { get; set; }
        public string? LOAN_AMOUNT { get; set; }
        public string? DISBURSALDATE { get; set; }
        public string? GuarantorName { get; set; }
        public DateTime? Last_Payment_Received { get; set; }
        public string? Guarantor_Contact_Details { get; set; }
        public string? Guarantor_Mailing_Address { get; set; }
        public string? NextDueDate { get; set; }
        public long? Loanperioddays { get; set; }
        public string? BounceCharges { get; set; }
        public string? Branch { get; set; }
        public string? AccountCategory { get; set; }
        public string? BranchCode { get; set; }
        public string? CentreID { get; set; }
        public string? GroupID { get; set; }
        public string? CentreName { get; set; }
        public string? GroupName { get; set; }
        public string? CreditCardNo { get; set; }
        public string? guarantoR1_CITY { get; set; }
        public string? fatherS_ADDRS { get; set; }
        public string? motherS_ADDRS { get; set; }
        public string? referencE1_ADDRS { get; set; }
        public string? physicaL_ADDRESS { get; set; }
        public string? newAddress { get; set; }
        public string? BOUNCED_CHEQUE_CHARGES { get; set; }
        public bool? IsEligibleForLegal { get; set; }
        public bool? IsEligibleForRepossession { get; set; }
        public bool? IsEligibleForRestructure { get; set; }
        public bool? IsEligibleForSettlement { get; set; }
        public string? RestructuringType { get; set; }
        public string? RestructureType { get; set; }
        public string? Restructuring_StartingMonth { get; set; }
        public string? Restructuring_EndingMonth { get; set; }
        public string? OS_Restructuring { get; set; }

        public string? DueDate { get; set; }
        public string? WRITEOFFDATE { get; set; }
        public string? DPD_STRING { get; set; }
        public string? AgencyAllocationExpiryDate { get; set; }
        public string? SZ_SPOUSE_NAME { get; set; }
        public string? INTEREST_RATE { get; set; }
        public string? LATE_PAYMENT_CHARGE { get; set; }
        public string? BVN { get; set; }
        public string? BILLING_CYCLE { get; set; }
        public string? CURRENT_DAYS_PAYMENT_DUE { get; set; }
        public string? PAST_DAYS_PAYMENT_DUE { get; set; }
        public string? PAYMENT_DUE_30_DAYS { get; set; }
        public string? PAYMENT_DUE_60_DAYS { get; set; }
        public string? PAYMENT_DUE_90_DAYS { get; set; }
        public string? PAYMENT_DUE_120_DAYS { get; set; }
        public string? PAYMENT_DUE_150_DAYS { get; set; }
        public string? PAYMENT_DUE_180_DAYS { get; set; }
        public string? PAYMENT_DUE_210_DAYS { get; set; }
        public string? WRITE_OFF_AMOUNT { get; set; }
        public string? RESIDENTIAL_CUSTOMER_CITY { get; set; }
        public string? RESIDENTIAL_CUSTOMER_STATE { get; set; }
        public string? RESIDENTIAL_PIN_CODE { get; set; }
        public string? RESIDENTIAL_COUNTRY { get; set; }
        public string? CUSTOMER_EMPLOYER { get; set; }
        public string? CUSTOMER_OFFICE_PIN_CODE { get; set; }
        public string? CUSTOMER_OFFICE_COUNTRY { get; set; }
        public string? CUSTOMERS_PERMANENT_EMAIL_ID { get; set; }
        public string? Partner_Name { get; set; }
        public string? Partner_Loan_ID { get; set; }
        public string? Partner_Customer_ID { get; set; }
        public string? TOTAL_LATE_PAYMENT_CHARGE { get; set; }
        public string? AssetType { get; set; }
        public string? AssetDetails { get; set; }
        public string? Latest_Number_From_Trail { get; set; }
        public string? Latest_Email_From_Trail { get; set; }
        public string? Latest_Address_From_Trail { get; set; }
        public string? Latest_Number_From_Receipt { get; set; }
        public string? Latest_Number_From_Send_Payment { get; set; }
        public string? Tenure { get; set; }
        public string? RepaymentMode { get; set; }
        public string? BounceChargesDue { get; set; }
        public string? BounceReason { get; set; }
        public decimal? Current_POS { get; set; }
        public string? WOFFFlag { get; set; }
        public string? PaymentStatus { get; set; }
        public string? BounceChargeOverdue { get; set; }
        public string? ResidentialAddress { get; set; }
        public string? ResidentialNumber { get; set; }
        public string? OfficeAddress { get; set; }
        public string? OfficeNumber { get; set; }
        public string? CoApplicantName { get; set; }
        public string? CoApplicantNumber { get; set; }
        public string? OTHER_CHARGES { get; set; }
        public string? NO_OF_EMI_OD { get; set; }
        public string? AccountJSON { get; set; }
    }
}