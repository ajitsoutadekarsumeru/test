using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchMyDepositSlipsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchMyDepositSlipsMapperConfiguration() : base()
        {
            CreateMap<PayInSlip, SearchMyDepositSlipsDto>()
                .ForMember(d => d.AccountNumber, s => s.MapFrom(s => s.BankAccountNo))
                .ForMember(d => d.Depositedate, s => s.MapFrom(s => s.DateOfDeposit))
                .ForMember(d => d.ENCslipNO, s => s.MapFrom(s => s.CMSPayInSlipNo));
        }
    }
}