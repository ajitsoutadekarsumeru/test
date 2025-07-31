using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ImportCollectionsDto : DtoBridge
    {
        public List<CollectionListDto> CollectionList { get; set; }
    }
    public partial class CollectionListDto  
    {
        public string ExternalSystemTransactionID { get; set; }
        public string AccountNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal ReceiptAmount { get; set; }
        public string ModeOfPayment { get; set; }
        public string? DraweeBankName { get; set; }
        public string? DraweeBranchName { get; set; }
        public string? DepositBranchCode { get; set; }
        public DateTime? InstrumentDate { get; set; }
        public string? InstrumentNo { get; set; }
        public string? PhysicalReceiptNumber { get; set; }
        public int? ENCAgencyID { get; set; }
        public int? ENCCollectorID { get; set; }
        public string? RelationShipWithCustomer { get; set; }
        public string? TaxRegistrationNumber { get; set; }
        public decimal? CollectionAgainstForeClosureAmount { get; set; }
        public decimal? CollectionAgainstEMIOverdueAmount { get; set; }
        public decimal? CollectionAgainstBounceCharges { get; set; }
        public decimal? CollectionAgainstPenalCharges { get; set; }
        public decimal? CollectionAgainstOtherCharges { get; set; }
        public decimal? CollectionAgainstSettlementAmount { get; set; }
        public decimal? CollectionAgainstCCBreakup1 { get; set; }
        public decimal? CollectionAgainstCCBreakup2 { get; set; }
        public decimal? CollectionAgainstCCBreakup3 { get; set; }
        public decimal? CollectionAgainstCCBreakup4 { get; set; }
        public decimal? CollectionAgainstCCBreakup5 { get; set; }
        public decimal? CollectionAgainstCCBreakup6 { get; set; }
        public int? WorkRequestId { get; set; }
        public int? Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
    }

}
