using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MyReceiptsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MyReceiptsMapperConfiguration() : base()
        {
            CreateMap<Collection, MyReceiptsDto>()
                .ForMember(o => o.BatchId, opt => opt.MapFrom(o => o.CollectionBatchId))
                .ForMember(o => o.CustomerAccountNo, opt => opt.MapFrom(o => o.Account.AGREEMENTID))
                .ForMember(o => o.EmiAmount, opt => opt.MapFrom(o => o.Account.EMIAMT))
                .ForMember(o => o.Product, opt => opt.MapFrom(o => o.Account.PRODUCT))
                .ForMember(o => o.ReceiptDate, opt => opt.MapFrom(o => o.CollectionDate))
                .ForMember(o => o.CollectorName, opt => opt.MapFrom(o => o.Collector.FirstName + "" + o.Collector.LastName))
                .ForMember(o => o.CollectorId, opt => opt.MapFrom(o => o.CollectorId))
                .ForMember(o => o.CollectorCode, opt => opt.MapFrom(o => o.Collector.CustomId))
                .ForMember(o => o.ReceivedAtAgency, opt => opt.MapFrom(o => o.AcknowledgedDate))
                .ForMember(o => o.ReceiptNo, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.BankName, opt => opt.MapFrom(o => o.Cheque.BankName))
                .ForMember(o => o.BranchName, opt => opt.MapFrom(o => o.Cheque.BranchName))
                .ForMember(o => o.InstrumentNo, opt => opt.MapFrom(o => o.Cheque.InstrumentNo))
                .ForMember(o => o.InstrumentDate, opt => opt.MapFrom(o => o.Cheque.InstrumentDate))
                .ForMember(o => o.BankCity, opt => opt.MapFrom(o => o.Cheque.BankCity))
                .ForMember(o => o.IfscCode, opt => opt.MapFrom(o => o.Cheque.IFSCCode))
                .ForMember(o => o.MicrCode, opt => opt.MapFrom(o => o.Cheque.MICRCode))
                .ForMember(o => o.OtherChanges, opt => opt.MapFrom(o => o.othercharges))
                .ForMember(o => o.CreditCardNo, opt => opt.MapFrom(o => o.Account.PRIMARY_CARD_NUMBER))
                .ForMember(o => o.TOS, opt => opt.MapFrom(o => !string.IsNullOrEmpty(o.Account.TOS) ? Convert.ToDecimal(Convert.ToString(o.Account.TOS)) : (decimal?)null))
                .ForMember(o => o.PTPAmount, opt => opt.MapFrom(o => o.Account.LatestPTPAmount))
                .ForMember(o => o.PTPDate, opt => opt.MapFrom(o => o.Account.LatestPTPDate != null ? Convert.ToDateTime(o.Account.LatestPTPDate) : (DateTime?)null))
                .ForMember(o => o.POS, opt => opt.MapFrom(o => o.Account.BOM_POS != null ? Convert.ToDecimal(o.Account.BOM_POS) : (decimal?)null))
                .ForMember(o => o.PaymentStatus, opt => opt.MapFrom(o => (o.CollectionBatch != null) ? (o.CollectionBatch.PayInSlip != null ?
                                                                            o.CollectionBatch.PayInSlip.PayInSlipWorkflowState.Name :
                                                                            o.CollectionWorkflowState.Name) : o.CollectionWorkflowState.Name))
                .ForMember(o => o.MinimumAmountDue, opt => opt.MapFrom(o => o.Account.CURRENT_MINIMUM_AMOUNT_DUE))
                .ForMember(o => o.TotalAmountDue, opt => opt.MapFrom(o => o.Account.CURRENT_TOTAL_AMOUNT_DUE))
                ;
        }
    }
}