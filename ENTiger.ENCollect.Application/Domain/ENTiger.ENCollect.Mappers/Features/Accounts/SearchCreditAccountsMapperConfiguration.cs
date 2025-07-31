using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchCreditAccountsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchCreditAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, SearchCreditAccountsDto>()
               .ForMember(dest => dest.TOS, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.TOS) ? 0 : Convert.ToDecimal(src.TOS)))

                .ForMember(dest => dest.AccountNo, opt => opt.MapFrom(src => src.CustomId))
                .ForMember(dest => dest.CreditCardNo, opt => opt.MapFrom(src => src.CustomId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CUSTOMERNAME))
                 .ForMember(dest => dest.EMIAmount, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.EMIAMT.ToString()) ? 0 : Convert.ToDecimal(src.EMIAMT.ToString())))
                .ForMember(dest => dest.CurrentBucket, opt => opt.MapFrom(src => src.CURRENT_BUCKET))
                 .ForMember(dest => dest.MonthStartingBucket, opt => opt.MapFrom(src => src.BUCKET.ToString()))
                  .ForMember(dest => dest.POS, opt => opt.MapFrom(src => src.BOM_POS))
                 .ForMember(dest => dest.MonthStartingBucket, opt => opt.MapFrom(src => src.BUCKET.ToString()))
                   .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.PRODUCT))
                      .ForMember(dest => dest.SubProductName, opt => opt.MapFrom(src => src.SubProduct))
                        //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.MAILINGADDRESS))
                        //  .ForMember(dest => dest.PERM_COUNTRY, opt => opt.MapFrom(src => src.PERMANENT_COUNTRY_CODE))
                        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CITY))
                      .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.STATE))
                     .ForMember(dest => dest.CurrentDPD, opt => opt.MapFrom(src => src.CURRENT_DPD.ToString()))
                      .ForMember(dest => dest.EMailId, opt => opt.MapFrom(src => src.LatestEmailId))
                       .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.LatestMobileNo))
                       .ForMember(dest => dest.PermanentMobileNo, opt => opt.MapFrom(src => src.MAILINGMOBILE))
                       .ForMember(dest => dest.PermanentMobileNo, opt => opt.MapFrom(src => src.MAILINGMOBILE))
                     .ForMember(dest => dest.PRINCIPLE_OVERDUE, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PRINCIPAL_OD.ToString()) ? 0 : Convert.ToDecimal(src.PRINCIPAL_OD)))
                     .ForMember(dest => dest.INTEREST_OVERDUE, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.INTEREST_OD.ToString()) ? 0 : Convert.ToDecimal(src.INTEREST_OD)))
                      .ForMember(dest => dest.CHARGE_OVERDUE, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.OTHER_CHARGES) ? 0 : Convert.ToDecimal(src.OTHER_CHARGES)))
                      .ForMember(dest => dest.TOTAL_OVERDUE_AMT, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.TOS) ? 0 : Convert.ToDecimal(src.TOS)))
                    //  .ForMember(dest => dest.statementedBalanceOs, opt => opt.MapFrom(src => src.STATEMENTED_OPENING_BALANCE.HasValue ? Convert.ToString(src.STATEMENTED_OPENING_BALANCE.Value) : null))
                    .ForMember(dest => dest.minimumAmountDue, opt => opt.MapFrom(src => src.CURRENT_MINIMUM_AMOUNT_DUE.HasValue ? Convert.ToString(src.CURRENT_MINIMUM_AMOUNT_DUE.Value) : null))
                    .ForMember(dest => dest.StatementedBucket, opt => opt.MapFrom(src => src.BUCKET.HasValue ? Convert.ToString(src.BUCKET.Value) : null));
            // .ForMember(dest => dest.CurrentBalanceOs, opt => opt.MapFrom(src => src.CURRENT_BALANCE_AMOUNT.HasValue ? Convert.ToString(src.CURRENT_BALANCE_AMOUNT.Value) : null));
        }
    }
}