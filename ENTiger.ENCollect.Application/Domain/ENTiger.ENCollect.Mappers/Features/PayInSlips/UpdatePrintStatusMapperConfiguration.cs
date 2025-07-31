using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdatePrintStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdatePrintStatusMapperConfiguration() : base()
        {
            CreateMap<UpdatePrintStatusDto, PayInSlip>();
        }
    }
}