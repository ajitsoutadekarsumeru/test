using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrintIdCardMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PrintIdCardMapperConfiguration() : base()
        {
            CreateMap<PrintIdCardDto, AgencyUser>();
        }
    }
}