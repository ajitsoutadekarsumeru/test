using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateDepositSlipMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CreateDepositSlipMapperConfiguration() : base()
        {
            CreateMap<CreateDepositSlipDto, PayInSlip>();
        }
    }
}