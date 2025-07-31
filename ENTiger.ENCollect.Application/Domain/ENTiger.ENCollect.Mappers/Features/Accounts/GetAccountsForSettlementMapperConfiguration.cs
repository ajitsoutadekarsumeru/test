using Microsoft.IdentityModel.Tokens;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetAccountsForSettlementMapperConfiguration : FlexMapperProfile
    {
        public GetAccountsForSettlementMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetAccountsForSettlementDto>()
                .ForMember(d => d.AccountId, s => s.MapFrom(a => a.Id))
                .ForMember(d => d.AccountNumber, s => s.MapFrom(a => a.AGREEMENTID))
                .ForMember(d => d.CustomerId, s => s.MapFrom(a => a.CUSTOMERID))
                .ForMember(d => d.ProductGroup, s => s.MapFrom(a => a.ProductGroup))
                .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
                .ForMember(d => d.CurrentDPD, s => s.MapFrom(a => a.CURRENT_DPD))
                .ForMember(d => d.NPAFlag, s => s.MapFrom(a => a.NPA_STAGEID == "Y" ? "Yes" : "No"))
                .ForMember(d => d.TOS, s => s.MapFrom(a => string.IsNullOrEmpty(a.TOS) ? 0 : Convert.ToDecimal(a.TOS)))
                .ForMember(d => d.POS, s => s.MapFrom(a => a.CURRENT_POS))
                .ForMember(d => d.TotalOverDue, s => s.MapFrom(a => a.CURRENT_TOTAL_AMOUNT_DUE))
                .ForMember(d => d.IsEligibleForSettlement, s => s.MapFrom(a => a.IsEligibleForSettlement.HasValue ? a.IsEligibleForSettlement : false));
        }
    }
}
