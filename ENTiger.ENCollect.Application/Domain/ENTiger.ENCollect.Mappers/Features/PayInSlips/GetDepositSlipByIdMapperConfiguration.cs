using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetDepositSlipByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetDepositSlipByIdMapperConfiguration() : base()
        {
            CreateMap<PayInSlip, GetDepositSlipByIdDto>()
                .ForMember(d => d.ID, s => s.MapFrom(s => s.Id))
                .ForMember(d => d.PayInSlipNo, s => s.MapFrom(s => s.CustomId))
                .ForMember(d => d.Latitude, s => s.MapFrom(s => s.Lattitude))
                .ForMember(d => d.DepositeBankName, s => s.MapFrom(s => s.BankName));
        }
    }
}