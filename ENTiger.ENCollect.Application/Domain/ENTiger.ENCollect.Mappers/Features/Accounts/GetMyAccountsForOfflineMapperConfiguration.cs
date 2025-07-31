using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetMyAccountsForOfflineMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetMyAccountsForOfflineMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetMyAccountsForOfflineDto>()
                     .ForMember(d => d.TOS, s => s.MapFrom(a => a.TOS))
                     .ForMember(d => d.AccountNo, s => s.MapFrom(a => a.CustomId))
                     .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
                     .ForMember(d => d.EMIAmount, s => s.MapFrom(a => a.EMIAMT))
                     .ForMember(d => d.CurrentBucket, s => s.MapFrom(a => a.CURRENT_BUCKET))
                     .ForMember(d => d.MonthStartingBucket, s => s.MapFrom(a => a.BUCKET))
                     .ForMember(d => d.POS, s => s.MapFrom(a => a.BOM_POS))
                     .ForMember(d => d.ProductName, s => s.MapFrom(a => a.PRODUCT))
                     .ForMember(d => d.City, s => s.MapFrom(a => a.CITY))
                     .ForMember(d => d.State, s => s.MapFrom(a => a.STATE))
                     .ForMember(d => d.CurrentDPD, s => s.MapFrom(a => a.CURRENT_DPD))
                     .ForMember(d => d.Id, s => s.MapFrom(a => a.Id))
                     .ForMember(d => d.EMailId, s => s.MapFrom(a => a.LatestEmailId))
                     .ForMember(d => d.MobileNo, s => s.MapFrom(a => a.LatestMobileNo))
                     .ForMember(d => d.PRINCIPLE_OVERDUE, s => s.MapFrom(a => a.PRINCIPAL_OD))
                     .ForMember(d => d.INTEREST_OVERDUE, s => s.MapFrom(a => a.INTEREST_OD))
                     .ForMember(d => d.PTPAmount, s => s.MapFrom(a => a.LatestPTPAmount))
                     .ForMember(d => d.LatestPTPAmount, s => s.MapFrom(a => a.LatestPTPAmount))
                     .ForMember(d => d.PTPDate, s => s.MapFrom(a => a.LatestPTPDate));
        }
    }
}