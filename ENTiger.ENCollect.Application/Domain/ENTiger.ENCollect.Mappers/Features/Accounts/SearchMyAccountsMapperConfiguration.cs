using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SearchMyAccountsMapperConfiguration : FlexMapperProfile
    {
        public SearchMyAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, SearchMyAccountsDto>()
                .ForMember(dest => dest.AccountNo, Dm => Dm.MapFrom(opt => opt.AGREEMENTID))
                .ForMember(dest => dest.Area, Dm => Dm.MapFrom(opt => opt.CITY))
                .ForMember(dest => dest.CustomerName, Dm => Dm.MapFrom(opt => opt.CUSTOMERNAME))
                .ForMember(dest => dest.PTPDate, Dm => Dm.MapFrom(opt => opt.LatestPTPDate))
                .ForMember(dest => dest.Bucket, Dm => Dm.MapFrom(opt => opt.BUCKET))
                .ForMember(dest => dest.POS, Dm => Dm.MapFrom(opt => opt.CURRENT_POS))
                .ForMember(d => d.MinimumAmountDue, s => s.MapFrom(a => a.CURRENT_MINIMUM_AMOUNT_DUE))
                .ForMember(d => d.TotalBalanceOutStanding, s => s.MapFrom(a => a.TOTAL_OUTSTANDING))
                .ForMember(d => d.CarediCardNo, s => s.MapFrom(a => a.PRIMARY_CARD_NUMBER))
                .ForMember(d => d.SubProductCode, s => s.MapFrom(a => a.SubProduct))
                .ForMember(d => d.PRODUCT, s => s.MapFrom(a => a.PRODUCT))
                .ForMember(d => d.ProductGroup, s => s.MapFrom(a => a.ProductGroup))
                .ForMember(d => d.SubProduct, s => s.MapFrom(a => a.SubProduct))
                //TODO: AgentAllocationExpiryDate should be changed to CollectorAllocationExpiryDate when fix has been implemented
                .ForMember(d => d.CollectorAllocationExpiryDate, s => s.MapFrom(a => a.AgentAllocationExpiryDate))
                  ;
        }
    }
}