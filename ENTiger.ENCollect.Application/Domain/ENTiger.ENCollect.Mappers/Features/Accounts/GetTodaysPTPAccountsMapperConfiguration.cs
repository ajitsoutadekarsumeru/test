using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTodaysPTPAccountsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTodaysPTPAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetTodaysPTPAccountsDto>()
            .ForMember(o => o.AccountNo, opt => opt.MapFrom(o => o.AGREEMENTID))
            .ForMember(o => o.Bucket, opt => opt.MapFrom(o => o.BUCKET))
            .ForMember(o => o.POS, opt => opt.MapFrom(o => o.CURRENT_POS))
            .ForMember(o => o.FirstName, opt => opt.MapFrom(o => o.CUSTOMERNAME))
            .ForMember(o => o.ProductCode, opt => opt.MapFrom(o => o.ProductCode))
            .ForMember(o => o.CreditCardNo, opt => opt.MapFrom(o => o.PRIMARY_CARD_NUMBER))
            .ForMember(o => o.PTP, opt => opt.MapFrom(o => o.LatestPTPDate))
            .ForMember(o => o.Area, opt => opt.MapFrom(o => o.CITY))
            .ForMember(o => o.Lattitude, opt => opt.MapFrom(o => o.LatestLatitude))
            .ForMember(o => o.Longitude, opt => opt.MapFrom(o => o.LatestLongitude))
            .ForMember(o => o.PTPAmount, opt => opt.MapFrom(o => o.LatestPTPAmount))
             .ForMember(d => d.CustomerID, s => s.MapFrom(a => a.CUSTOMERID))
             .ForMember(d => d.TotalOverDueAmount, s => s.MapFrom(a => a.CURRENT_TOTAL_AMOUNT_DUE));
        }
    }
}