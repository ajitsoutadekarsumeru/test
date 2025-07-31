using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdatePayInSlipMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdatePayInSlipMapperConfiguration() : base()
        {
            CreateMap<UpdatePayInSlipDto, PayInSlip>();
        }
    }
}