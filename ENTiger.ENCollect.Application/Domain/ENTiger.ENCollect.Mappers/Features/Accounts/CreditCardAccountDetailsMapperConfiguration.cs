using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreditCardAccountDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CreditCardAccountDetailsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, CreditCardAccountDetailsDto>()
            .ForMember(d => d.AccountNo, s => s.MapFrom(a => a.AGREEMENTID))
            .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
            .ForMember(d => d.CreditCardNo, s => s.MapFrom(a => a.PRIMARY_CARD_NUMBER))
            .ForMember(d => d.CustomerId, s => s.MapFrom(a => a.CUSTOMERID))
            .ForMember(d => d.ProductName, s => s.MapFrom(a => a.PRODUCT))
            .ForMember(d => d.PermanentMobileNo, s => s.MapFrom(a => a.MAILINGMOBILE))
            .ForMember(d => d.EMailId, s => s.MapFrom(a => a.LatestEmailId))
            .ForMember(d => d.minimumAmountDue, s => s.MapFrom(a => a.CURRENT_MINIMUM_AMOUNT_DUE))
            .ForMember(d => d.TOTAL_OVERDUE_AMT, s => s.MapFrom(a => a.CURRENT_TOTAL_AMOUNT_DUE))
            .ForMember(d => d.CycleDate, s => s.MapFrom(a => a.BILLING_CYCLE))
            .ForMember(d => d.MonthStartingBucket, s => s.MapFrom(a => a.BUCKET))
            .ForMember(d => d.CurrentBucket, s => s.MapFrom(a => a.CURRENT_BUCKET))
            .ForMember(d => d.PTPAmount, s => s.MapFrom(a => a.LatestPTPAmount))

            .ForMember(d => d.EMIAmount, s => s.MapFrom(a => a.EMIAMT))
            .ForMember(d => d.POS, s => s.MapFrom(a => a.BOM_POS))
            .ForMember(d => d.SubProductName, s => s.MapFrom(a => a.SubProduct))
            .ForMember(d => d.City, s => s.MapFrom(a => a.CITY))
            .ForMember(d => d.State, s => s.MapFrom(a => a.STATE))
            .ForMember(d => d.CurrentDPD, s => s.MapFrom(a => a.CURRENT_DPD))
            .ForMember(d => d.PRINCIPLE_OVERDUE, s => s.MapFrom(a => a.PRINCIPAL_OD))
            .ForMember(d => d.INTEREST_OVERDUE, s => s.MapFrom(a => a.INTEREST_OD))
            .ForMember(d => d.CHARGE_OVERDUE, s => s.MapFrom(a => a.OTHER_CHARGES))
            .ForMember(d => d.PENAL_PENDING, s => s.MapFrom(a => a.PENAL_PENDING))
            .ForMember(d => d.EMI_OD_AMT, s => s.MapFrom(a => a.EMI_OD_AMT))
            .ForMember(d => d.StatementedBucket, s => s.MapFrom(a => a.BUCKET))
            .ForMember(d => d.MobileNo, s => s.MapFrom(a => a.LatestMobileNo))

            .ForMember(d => d.State, s => s.MapFrom(a => a.STATE))
            .ForMember(d => d.City, s => s.MapFrom(a => a.CITY))
            .ForMember(d => d.NewAddressFromTrails, s => s.MapFrom(a => a.Latest_Address_From_Trail));
        }
    }
}