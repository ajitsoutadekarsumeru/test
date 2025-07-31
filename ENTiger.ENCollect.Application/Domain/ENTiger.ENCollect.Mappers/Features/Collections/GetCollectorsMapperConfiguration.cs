using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectorsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCollectorsMapperConfiguration() : base()
        {
            CreateMap<ApplicationUser, GetCollectorsDto>();
        }
    }
}