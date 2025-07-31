using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyDashBoardMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyDashBoardMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, AccountsLookupDto>()
            .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
            .ForMember(d => d.CustomerID, s => s.MapFrom(a => a.CUSTOMERID))
            .ForMember(d => d.CustomerAccountNo, s => s.MapFrom(a => a.CustomId))
            .ForMember(d => d.Product, s => s.MapFrom(a => a.PRODUCT))
            .ForMember(d => d.CurrentBucket, s => s.MapFrom(a => a.CURRENT_BUCKET))
            .ForMember(d => d.MonthOpeningBucket, s => s.MapFrom(a => a.BUCKET))
            .ForMember(d => d.EmiAmount, s => s.MapFrom(a => a.EMIAMT))
            .ForMember(d => d.MonthOpeningPOS, s => s.MapFrom(a => a.BOM_POS))
            .ForMember(d => d.CurrentPOS, s => s.MapFrom(a => a.CURRENT_POS))
            .ForMember(d => d.AgencyCode, s => s.MapFrom(a => a.Agency.CustomId))
            .ForMember(d => d.NoOfEMIDue, s => s.MapFrom(a => a.NO_OF_EMI_OD))
            .ForMember(d => d.NpaStageID, s => s.MapFrom(a => a.NPA_STAGEID))
            .ForMember(d => d.OverdueEmiAmount, s => s.MapFrom(a => a.EMI_OD_AMT))
            .ForMember(d => d.CollectorName, s => s.MapFrom(a => a.Collector.FirstName))
            .ForMember(d => d.TeleCallingAgencyCode, s => s.MapFrom(a => a.TeleCallingAgency.CustomId))
            .ForMember(d => d.TeleCallerName, s => s.MapFrom(a => a.TeleCaller.FirstName))
            .ForMember(d => d.CustCardNo, s => s.MapFrom(a => a.PRIMARY_CARD_NUMBER))
            .ForMember(d => d.MinimumAmountDue, s => s.MapFrom(a => a.CURRENT_MINIMUM_AMOUNT_DUE))
            .ForMember(d => d.AgencyName, s => s.MapFrom(a => a.Agency.FirstName))
            .ForMember(d => d.AllocationOwnerName, s => s.MapFrom(a => a.AllocationOwner.FirstName))
            .ForMember(d => d.AllocationOwnerCode, s => s.MapFrom(a => a.AllocationOwner.CustomId))
            .ForMember(d => d.NoOfEMIDue, s => s.MapFrom(a => a.NO_OF_EMI_OD))
            .ForMember(d => d.PartnerLoanId, s => s.MapFrom(a => a.Partner_Loan_ID))
            .ForMember(d => d.CreditCardNumber, s => s.MapFrom(a => a.PRIMARY_CARD_NUMBER))
            .ForMember(d => d.Bucket, s => s.MapFrom(a => a.BUCKET))
            .ForMember(d => d.Cycle, s => s.MapFrom(a => a.BILLING_CYCLE))
            .ForMember(d => d.TotalOverdueAmount, s => s.MapFrom(a => a.CURRENT_TOTAL_AMOUNT_DUE))
            .ForMember(d => d.Current_DPD, s => s.MapFrom(a => a.CURRENT_DPD))
            .ForMember(d => d.ProductGroup, s => s.MapFrom(a => a.ProductCode))
            ;

        }
    }
}