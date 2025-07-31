using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PayInSlipMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PayInSlipMapperConfiguration() : base()
        {
            CreateMap<PayInSlipDto, PayInSlip>();
            CreateMap<PayInSlip, PayInSlipDto>();
            CreateMap<PayInSlipDtoWithId, PayInSlip>();
            CreateMap<PayInSlip, PayInSlipDtoWithId>();
        }
    }
}