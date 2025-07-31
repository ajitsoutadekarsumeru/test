using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountsByFilterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAccountsByFilterMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, SearchAccountsByFilterDto>()
            .ForMember(d => d.AccountNo, s => s.MapFrom(a => a.CustomId))
            .ForMember(d => d.CustomerId, s => s.MapFrom(a => a.CUSTOMERID))
            .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
            .ForMember(d => d.CurrentDPD, s => s.MapFrom(a => a.CURRENT_DPD))
            .ForMember(d => d.EMIAmount, s => s.MapFrom(a => a.EMIAMT))
            .ForMember(d => d.CurrentBucket, s => s.MapFrom(a => a.CURRENT_BUCKET))
            .ForMember(d => d.MonthStartingBucket, s => s.MapFrom(a => a.BUCKET))
            .ForMember(d => d.POS, s => s.MapFrom(a => a.BOM_POS))
            .ForMember(d => d.ProductName, s => s.MapFrom(a => a.PRODUCT))
            .ForMember(d => d.City, s => s.MapFrom(a => a.CITY))
            .ForMember(d => d.State, s => s.MapFrom(a => a.STATE))
            .ForMember(d => d.MobileNo, s => s.MapFrom(a => a.LatestMobileNo))
            .ForMember(d => d.PermanentMobileNo, s => s.MapFrom(a => a.MAILINGMOBILE))
            .ForMember(d => d.PTPAmount, s => s.MapFrom(a => a.LatestPTPAmount))
            .ForMember(d => d.PTPDate, s => s.MapFrom(a => a.LatestPTPDate))
            .ForMember(d => d.AmountDue, s => s.MapFrom(a => a.CURRENT_MINIMUM_AMOUNT_DUE))
            .ForMember(d => d.LastStatementDate, s => s.MapFrom(a => a.LAST_STATEMENT_DATE))
            .ForMember(d => d.LastStatementDueDate, s => s.MapFrom(a => a.LAST_STATEMENT_DATE))
            .ForMember(d => d.StatementedBucket, s => s.MapFrom(a => a.BUCKET))
            .ForMember(d => d.CreditCardNo, s => s.MapFrom(a => a.ReverseOfPrimaryCard))
            .ForMember(d => d.CreditCardNumber, s => s.MapFrom(a => a.PRIMARY_CARD_NUMBER))
            .ForMember(d => d.TOS, s => s.MapFrom(a => a.TOS))
            .ForMember(d => d.Branch, s => s.MapFrom(a => a.BRANCH))
            .ForMember(d => d.AccountCategory, s => s.MapFrom(a => a.SCHEME_DESC))
            .ForMember(d => d.PartnerLoanId, s => s.MapFrom(a => a.Partner_Loan_ID))
            .ForMember(d => d.MAD, s => s.MapFrom(a => a.CURRENT_MINIMUM_AMOUNT_DUE))
            .ForMember(d => d.TAD, s => s.MapFrom(a => a.CURRENT_TOTAL_AMOUNT_DUE))
            .ForMember(d => d.CollectorAllocationExpiryDate, s => s.MapFrom(a => a.AgentAllocationExpiryDate));
        }
    }
}