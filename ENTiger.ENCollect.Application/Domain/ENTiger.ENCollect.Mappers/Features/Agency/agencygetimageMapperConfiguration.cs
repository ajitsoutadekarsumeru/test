using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class agencygetimageMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public agencygetimageMapperConfiguration() : base()
        {
            CreateMap<agencygetimageDto, Agency>();
        }
    }
}