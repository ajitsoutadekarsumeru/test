using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAttemptedMyAccountsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAttemptedMyAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetAttemptedMyAccountsDto>()
                .ForMember(d => d.Bucket, s => s.MapFrom(a => a.BUCKET))
                .ForMember(d => d.POS, s => s.MapFrom(a => a.BOM_POS))
                .ForMember(d => d.PTPDate, s => s.MapFrom(a => a.LatestPTPDate))
                .ForMember(d => d.ProductCode, s => s.MapFrom(a => a.ProductCode))
                .ForMember(d => d.carediCardNo, s => s.MapFrom(a => a.CustomId))
                .ForMember(d => d.AccountNo, s => s.MapFrom(a => a.AGREEMENTID))
                .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
                .ForMember(d => d.Area, s => s.MapFrom(a => a.CITY))
                  //TODO: AgentAllocationExpiryDate should be changed to CollectorAllocationExpiryDate when fix has been implemented
                  .ForMember(d => d.CollectorAllocationExpiryDate, s => s.MapFrom(a => a.AgentAllocationExpiryDate))
                  .ForMember(d => d.CustomerID, s => s.MapFrom(a => a.CUSTOMERID))
                ;
        }
    }
}