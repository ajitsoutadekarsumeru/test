using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreatePayInSlipMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CreatePayInSlipMapperConfiguration() : base()
        {
            CreateMap<CreatePayInSlipDto, PayInSlip>();
        }
    }
}