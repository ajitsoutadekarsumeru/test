using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeactivateAgencyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DeactivateAgencyMapperConfiguration() : base()
        {
            CreateMap<DeactivateAgencyDto, Agency>();
        }
    }
}