using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMoneyMovementDetailsDto : DtoBridge
    {
        public string? ProductGroup { get; set; }
        public string? Product { get; set; }
        public string? SubProduct { get; set; }
        public string? AccountCustomerId { get; set; }
        public string? AccountAgreementNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
        public string? Region { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? AgentName { get; set; }
        public string? AgentCode { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? PhysicalReceiptNumber { get; set; }
        public string? CollectionDate { get; set; }
        public string? CurrentBucket { get; set; }
        public string? OverdueAmountEmiAmount { get; set; }
        public string? AmountBreakupOne { get; set; }
        public string? ForeClosureAmount { get; set; }


        public string? SettlementAmount { get; set; }

        public string? LatePaymentPenalty { get; set; }
        public string? OtherCharges { get; set; }

        public decimal? TotalReceiptAmount { get; set; }

        public string? PaymentMode { get; set; }
        
        public string? InstrumentDate { get; set; }
        public string? InstrumentNumber { get; set; }
        public decimal? InstrumentAmount { get; set; }
        public string? MicrCode { get; set; }
        public string? BatchId { get; set; }
        public string? BatchIdCreatedDate { get; set; }
        public string? DepositDate { get; set; }
        public decimal? BatchAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public int? BomBucket { get; set; }
        public string? NPAStageId { get; set; }
        public string? LatestLatitude { get; set; }
        public string? LatestLongitude { get; set; }
        public string? PrimaryCardNumber { get; set; }
        public string? AgentEmail { get; set; }
        public string? PaymentTowards { get; set; }
        public string? BounceCharges { get; set; }
        public string? Excess { get; set; }
        public string? Imd { get; set; }
        public string? ProcFee { get; set; }
        public string? Swap { get; set; }
        public string? EbcCharge { get; set; }
        public string? CollectionPickupCharge { get; set; }
        public string? EncollectPayInSlipId { get; set; }
        public string? CmsPayinSlipNumber { get; set; }
        public string? DepositAccountNumber { get; set; }
        public string? DepositBankName { get; set; }
        public decimal? DepositAmount { get; set; }
        public string? MerchantReferenceNumber { get; set; }
        public string? BankTransactionId { get; set; }
        public string? BankId { get; set; }
        public decimal? CollectionsAmount { get; set; }
        public string? StatusCode { get; set; }
        public string? CreatedDate { get; set; }
        public string? Rrn { get; set; }
        public string? CardHolderName { get; set; }
        public string? MerchantId { get; set; }
        public string? MerchantTransactionId { get; set; }
        public string? AgencyName { get; set; }

        public string? AgencyCode { get; set; }
        public string? StaffOrAgent { get; set; }
        
    }
}
