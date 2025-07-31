using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionsForCancellationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCollectionsForCancellationMapperConfiguration() : base()
        {
            CreateMap<Collection, GetCollectionsForCancellationDto>()
                .ForMember(o => o.ReceiptNo, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.ReceiptDate, opt => opt.MapFrom(o => o.CollectionDate.Value.Date.ToString("yyyy-MM-dd")))
                .ForMember(o => o.CollectorName, opt => opt.MapFrom(o => o.Collector.FirstName + " " + o.Collector.LastName))
                .ForMember(o => o.CustomerAccountNo, opt => opt.MapFrom(o => o.Account.AGREEMENTID))
                .ForMember(o => o.ProductName, opt => opt.MapFrom(o => o.Account.PRODUCT))
                .ForMember(o => o.EmiAmount, opt => opt.MapFrom(o => o.Account.EMIAMT))
                .ForMember(o => o.Total, opt => opt.MapFrom(o => o.Amount));
        }
    }
}