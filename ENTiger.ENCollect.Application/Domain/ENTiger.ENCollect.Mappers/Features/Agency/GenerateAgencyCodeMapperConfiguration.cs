using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GenerateAgencyCodeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GenerateAgencyCodeMapperConfiguration() : base()
        {
            CreateMap<Agency, GenerateAgencyCodeDto>();
        }
    }
}