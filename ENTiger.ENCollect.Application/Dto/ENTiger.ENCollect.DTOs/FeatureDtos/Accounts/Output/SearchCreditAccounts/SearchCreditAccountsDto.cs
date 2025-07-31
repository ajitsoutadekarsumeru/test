using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCreditAccountsDto : DtoBridge
    {
        // public Boolean PropensityToPay { get; set; }
        // public float? PropensityToPayConfidence { get; set; }
        public decimal? CHARGE_OVERDUE { get; set; }

        public decimal? TOTAL_OVERDUE_AMT { get; set; }
        public decimal? ARREAR { get; set; }
        public string? PENAL_PENDING { get; set; }
        public decimal? CURRENT_POS { get; set; }
        public string? EMI_OD_AMT { get; set; }
        public decimal? LatestPTPAmount { get; set; }
        public string? CO_BOR_NAME { get; set; }
        public string? CO_BOR_PHONE { get; set; }
        public string? CO_BOR_PHONE_2 { get; set; }
        public string? CO_BOR_PHONE_3 { get; set; }
        public string? CO_BOR_PHONE_4 { get; set; }
        public string? CO_BOR_PERM_ADDRESS_LINE1 { get; set; }
        public decimal? INTEREST_OVERDUE { get; set; }
        public string? CO_BOR_PERM_ADDRESS_LINE2 { get; set; }
        public string? CO_BOR_PERM_CITY { get; set; }
        public string? CO_BOR_PERM_STATE { get; set; }
        public string? CO_BOR_PERM_COUNTRY { get; set; }
        public string? CO_BOR_PERM_PIN { get; set; }

        //public string? MAILINGPHONE1 { get; set; }
        //public string? MAILINGPHONE2 { get; set; }
        public string? PHONE_3 { get; set; }

        public string? PHONE_4 { get; set; }

        //public string? NONMAILINGADDRESS { get; set; }
        //public string? NONMAILINGLANDMARK { get; set; }
        public string? PERM_ADDRESS_LINE3 { get; set; }

        // public string? NONMAILINGCITY { get; set; }
        //   public string? NONMAILINGSTATE { get; set; }
        public string? CO_BOR_PERM_ADDRESS_LINE3 { get; set; }

        public decimal? PRINCIPLE_OVERDUE { get; set; }
        public bool? IsDepositAccount { get; set; }
        public bool? IsPoolAccount { get; set; }
        public string? AccountNo { get; set; }
        public string? CreditCardNo { get; set; }
        public string? Address { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? ContractId { get; set; }
        public string? CurrentBucket { get; set; }
        public string? CurrentDPD { get; set; }
        public string? CustomerName { get; set; }

        [StringLength(100)]
        public string? EMailId { get; set; }

        public string? EMIAmount { get; set; }
        public string? DMConsumerId { get; set; }
        public string? DMAccountId { get; set; }

        //public string? BCC_PENDING { get; set; }
        public string? Id { get; set; }

        public string? MobileNo { get; set; }
        public string? PermanentMobileNo { get; set; }
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

        // public string? DepositBankName { get; set; }
        public string? LegalAndSettlementStatus { get; set; }

        public string? DepositBankBranchId { get; set; }
        public string? PERM_COUNTRY { get; set; }
        public DateTime? EMI_DUE_DATE { get; set; }
        public string? statementedBalanceOs { get; set; }
        public string? minimumAmountDue { get; set; }
        public string? StatementedBucket { get; set; }
        public string? CurrentBalanceOs { get; set; }
        public bool? IsEligibleForRepossession { get; set; }
        public bool? IsEligibleForLegal { get; set; }
        public bool? IsEligibleForRestructure { get; set; }
        public bool? IsEligibleForSettlement { get; set; }
    }
}