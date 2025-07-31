using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectAgencyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public RejectAgencyMapperConfiguration() : base()
        {
            CreateMap<RejectAgencyDto, Agency>();
        }
    }
}