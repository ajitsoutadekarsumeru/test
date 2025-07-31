using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPayInSlipsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetPayInSlipsMapperConfiguration() : base()
        {
            CreateMap<PayInSlip, GetPayInSlipsDto>()
                .ForMember(vm => vm.payInSlipCode, Dm => Dm.MapFrom(dModel => dModel.CustomId));
        }
    }
}