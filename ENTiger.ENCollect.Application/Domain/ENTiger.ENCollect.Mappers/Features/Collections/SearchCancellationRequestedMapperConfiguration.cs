using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCancellationRequestedMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCancellationRequestedMapperConfiguration() : base()
        {
            CreateMap<Collection, SearchCancellationRequestedDto>()
                .ForMember(cm => cm.ReceiptNo, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                .ForMember(cm => cm.ReceiptId, Dm => Dm.MapFrom(dModel => dModel.ReceiptId))
                .ForMember(cm => cm.ReceiptDate, Dm => Dm.MapFrom(dModel => dModel.CollectionDate))
                .ForMember(cm => cm.OverdueAmount, Dm => Dm.MapFrom(dModel => dModel.yOverdueAmount))
                .ForMember(cm => cm.BounceCharges, Dm => Dm.MapFrom(dModel => dModel.yBounceCharges))
                .ForMember(cm => cm.PenalAmount, Dm => Dm.MapFrom(dModel => dModel.yPenalInterest))
                .ForMember(cm => cm.ForeclosureAmount, Dm => Dm.MapFrom(dModel => dModel.yForeClosureAmount))
                .ForMember(cm => cm.CustomerAccountNo, Dm => Dm.MapFrom(dModel => dModel.Account.CustomId))
                .ForMember(cm => cm.AgencyCode, Dm => Dm.MapFrom(dModel => dModel.CollectionOrg.CustomId))
                .ForMember(cm => cm.AgencyName, Dm => Dm.MapFrom(dModel => dModel.CollectionOrg.FirstName + " " + dModel.CollectionOrg.LastName))
                .ForMember(cm => cm.CollectorName, Dm => Dm.MapFrom(dModel => dModel.Collector.FirstName + " " + dModel.Collector.LastName))
                .ForMember(cm => cm.CollectorCustomId, Dm => Dm.MapFrom(dModel => dModel.Collector.CustomId))
                .ForMember(cm => cm.MobileNo, Dm => Dm.MapFrom(dModel => dModel.MobileNo))
                .ForMember(cm => cm.EmailId, Dm => Dm.MapFrom(dModel => dModel.EMailId))
                .ForMember(cm => cm.Total, Dm => Dm.MapFrom(dModel => dModel.Amount))
                .ForMember(cm => cm.EmiAmount, Dm => Dm.MapFrom(dModel => dModel.Account.EMIAMT))
                .ForMember(cm => cm.ProductName, Dm => Dm.MapFrom(dModel => dModel.Account.PRODUCT))
                .ForMember(cm => cm.AgencyCode, Dm => Dm.MapFrom(dModel => dModel.CollectionOrg.CustomId))
                .ForMember(cm => cm.AgencyName, Dm => Dm.MapFrom(dModel => dModel.CollectionOrg.FirstName))
                .ForMember(cm => cm.RecieptCancellationRequestDate, Dm => Dm.MapFrom(dModel => dModel.LastModifiedDate))
                .ForMember(cm => cm.CancellationRemarks, Dm => Dm.MapFrom(dModel => dModel.CancellationRemarks))
                ;
        }
    }
}