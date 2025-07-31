using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ZoneListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ZoneListMapperConfiguration() : base()
        {
            CreateMap<Zone, ZoneListDto>();
        }
    }
}