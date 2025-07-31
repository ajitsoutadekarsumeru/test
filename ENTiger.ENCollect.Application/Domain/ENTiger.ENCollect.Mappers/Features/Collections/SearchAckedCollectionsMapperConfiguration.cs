using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAckedCollectionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAckedCollectionsMapperConfiguration() : base()
        {
            CreateMap<Collection, SearchAckedCollectionsDto>()
                .ForMember(o => o.ReceiptNo, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.CollectorId, opt => opt.MapFrom(o => o.Collector.CustomId))
                .ForMember(o => o.PaymentMode, opt => opt.MapFrom(o => o.CollectionMode))
                .ForMember(o => o.ReceiptDate, opt => opt.MapFrom(o => o.CollectionDate))
                .ForMember(o => o.OverdueAmount, opt => opt.MapFrom(o => o.yOverdueAmount))
                .ForMember(o => o.DateReceivedAtAgency, opt => opt.MapFrom(o => o.AcknowledgedDate))
                .ForMember(o => o.ForeclosureAmount, opt => opt.MapFrom(o => o.yForeClosureAmount))
                .ForMember(o => o.BounceCharges, opt => opt.MapFrom(o => o.yBounceCharges))
                .ForMember(o => o.PenalAmount, opt => opt.MapFrom(o => o.yPenalInterest))
                .ForMember(o => o.CollectorFistName, opt => opt.MapFrom(o => o.Collector.FirstName))
                .ForMember(o => o.CollectorMiddleName, opt => opt.MapFrom(o => o.Collector.MiddleName))
                .ForMember(o => o.CollectorLastName, opt => opt.MapFrom(o => o.Collector.LastName))
                .ForMember(o => o.AccountNo, opt => opt.MapFrom(o => o.Account.AGREEMENTID))
                .ForMember(o => o.InstrumentNo, opt => opt.MapFrom(o => o.Cheque.InstrumentNo))
                .ForMember(o => o.DraweeBank, opt => opt.MapFrom(o => o.Cheque.BankName))
                .ForMember(o => o.DraweeBranch, opt => opt.MapFrom(o => o.Cheque.BranchName))
                .ForMember(o => o.ProductGroup, opt => opt.MapFrom(o => o.Account.ProductGroup))
                .ForMember(o => o.SettlementAmount, opt => opt.MapFrom(o => o.Settlement));
        }
    }
}