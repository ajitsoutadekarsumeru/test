using Elastic.Clients.Elasticsearch.Snapshot;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetSettlementEligibleAccountsMapperConfiguration : FlexMapperProfile
    {
        public GetSettlementEligibleAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetSettlementEligibleAccountsDto>()
                .ForMember(d => d.AccountId, s => s.MapFrom(a => a.Id))
                .ForMember(d => d.AccountNumber, s => s.MapFrom(a => a.AGREEMENTID))
                .ForMember(d => d.ProductGroup, s => s.MapFrom(a => a.ProductGroup))
                .ForMember(d => d.CustomerId, s => s.MapFrom(a => a.CUSTOMERID))
                .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
                .ForMember(d => d.CurrentDPD, s => s.MapFrom(a => a.CURRENT_DPD))
                .ForMember(d => d.TOS, s => s.MapFrom(a => string.IsNullOrEmpty(a.TOS) ? 0 : Convert.ToDecimal(a.TOS)))
                .ForMember(d => d.NPAFlag, s => s.MapFrom(a => a.NPA_STAGEID == "Y" ? "Yes" : "No"))
                .ForMember(d => d.FlaggedAsEligible, s => s.MapFrom(a => a.IsEligibleForSettlement.HasValue && a.IsEligibleForSettlement == true ? "Yes" : "No"))

                .ForMember(d => d.MobileNumber, s => s.MapFrom(a => a.LatestMobileNo))
                .ForMember(d => d.CurrentBucket, s => s.MapFrom(a => string.IsNullOrEmpty(a.CURRENT_BUCKET) ? 0 : Convert.ToInt16(a.CURRENT_BUCKET)))
                .ForMember(d => d.InterestOutStanding, s => s.MapFrom(a => a.INTEREST_OD))
                .ForMember(d => d.ChargesOutStanding, s => s.MapFrom(a => string.IsNullOrEmpty(a.OTHER_CHARGES) ? 0 : Convert.ToDecimal(a.OTHER_CHARGES)))
                .ForMember(d => d.POS, s => s.MapFrom(a => a.CURRENT_POS))
                .ForMember(d => d.NoOfEMIDue, s => s.MapFrom(a => string.IsNullOrEmpty(a.NO_OF_EMI_OD) ? 0 : Convert.ToInt16(a.NO_OF_EMI_OD)))

                .ForMember(d => d.IsSettlementCreated, s => s.MapFrom(a => a.Settlements.Where(s => s.Status.Contains(s.Status)).Count() > 0 ? true : false));

            var settlementOpenStatusSet = SettlementStatusEnum.ByGroup("Open").Select(s => s.Value).ToHashSet();
        }
    }
}
