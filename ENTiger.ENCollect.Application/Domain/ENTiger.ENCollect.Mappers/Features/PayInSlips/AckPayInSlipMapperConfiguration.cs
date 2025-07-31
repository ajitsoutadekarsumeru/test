using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AckPayInSlipMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AckPayInSlipMapperConfiguration() : base()
        {
            CreateMap<AckPayInSlipDto, PayInSlip>();
        }
    }
}