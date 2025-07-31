using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetByCustomerIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetByCustomerIdMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetByCustomerIdDto>()
            .ForMember(d => d.Id, s => s.MapFrom(a => a.Id))
            .ForMember(d => d.AccountNo, s => s.MapFrom(a => a.AGREEMENTID))
            .ForMember(d => d.CustomerID, s => s.MapFrom(a => a.CUSTOMERID))
            .ForMember(d => d.CustomerName, s => s.MapFrom(a => a.CUSTOMERNAME))
            .ForMember(d => d.TotalOverdueAmount, s => s.MapFrom(a => a.CURRENT_TOTAL_AMOUNT_DUE))
            .ForMember(d => d.ProductGroup, s => s.MapFrom(a => a.ProductCode))
            .ForMember(d => d.Current_DPD, s => s.MapFrom(a => a.CURRENT_DPD))
            .ForMember(d => d.CurrentBucket, s => s.MapFrom(a => a.CURRENT_BUCKET))
            .ForMember(d => d.Product, s => s.MapFrom(a => a.PRODUCT))
            .ForMember(d => d.CurrentPOS, s => s.MapFrom(a => a.CURRENT_POS))
            ;
        }
    }
}