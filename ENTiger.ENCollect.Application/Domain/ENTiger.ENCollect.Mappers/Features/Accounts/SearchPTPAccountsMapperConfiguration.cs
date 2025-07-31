using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchPTPAccountsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchPTPAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, SearchPTPAccountsDto>()
            .ForMember(o => o.Id, opt => opt.MapFrom(o => o.Id))
            .ForMember(o => o.AccountNo, opt => opt.MapFrom(o => o.AGREEMENTID))
            .ForMember(o => o.Bucket, opt => opt.MapFrom(o => o.BUCKET))
            .ForMember(o => o.POS, opt => opt.MapFrom(o => o.CURRENT_POS))
            .ForMember(o => o.FirstName, opt => opt.MapFrom(o => o.CUSTOMERNAME))
            .ForMember(o => o.ProductCode, opt => opt.MapFrom(o => o.ProductCode))
            .ForMember(o => o.CreditCardNo, opt => opt.MapFrom(o => o.CustomId))
            .ForMember(o => o.PTP, opt => opt.MapFrom(o => o.LatestPTPDate))
            .ForMember(o => o.PTPAmount, opt => opt.MapFrom(o => o.LatestPTPAmount))
            .ForMember(o => o.Area, opt => opt.MapFrom(o => o.CITY))
            .ForMember(o => o.Lattitude, opt => opt.MapFrom(o => o.LatestLatitude))
            .ForMember(o => o.Longitude, opt => opt.MapFrom(o => o.LatestLongitude));
        }
    }
}