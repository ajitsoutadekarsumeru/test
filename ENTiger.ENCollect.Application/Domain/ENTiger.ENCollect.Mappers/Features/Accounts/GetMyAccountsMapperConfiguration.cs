using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetMyAccountsMapperConfiguration : FlexMapperProfile
    {
        public GetMyAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetMyAccountsDto>()
                  .ForMember(d => d.Bucket, s => s.MapFrom(a => a.BUCKET))
                  .ForMember(d => d.POS, s => s.MapFrom(a => a.CURRENT_POS))
                  .ForMember(d => d.PTPDate, s => s.MapFrom(a => a.LatestPTPDate))
                  .ForMember(d => d.AccountNo, s => s.MapFrom(a => a.AGREEMENTID))
                  .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
                  .ForMember(d => d.Area, s => s.MapFrom(a => a.CITY))
                  .ForMember(d => d.MinimumAmountDue, s => s.MapFrom(a => a.CURRENT_MINIMUM_AMOUNT_DUE))
                  .ForMember(d => d.TotalBalanceOutStanding, s => s.MapFrom(a => a.TOTAL_OUTSTANDING))
                  .ForMember(d => d.CarediCardNo, s => s.MapFrom(a => a.PRIMARY_CARD_NUMBER))
                  .ForMember(d => d.ProductCode, s => s.MapFrom(a => a.ProductCode))
                  .ForMember(d => d.SubProductCode, s => s.MapFrom(a => a.SubProduct))
                  .ForMember(d => d.PRODUCT, s => s.MapFrom(a => a.PRODUCT))
                  .ForMember(d => d.ProductGroup, s => s.MapFrom(a => a.ProductGroup))
                  .ForMember(d => d.SubProduct, s => s.MapFrom(a => a.SubProduct))
                  //TODO: AgentAllocationExpiryDate should be changed to CollectorAllocationExpiryDate when fix has been implemented
                  .ForMember(d => d.CollectorAllocationExpiryDate, s => s.MapFrom(a => a.AgentAllocationExpiryDate))
                   .ForMember(d => d.CustomerID, s => s.MapFrom(a => a.CUSTOMERID))
                  ;
        }
    }
}