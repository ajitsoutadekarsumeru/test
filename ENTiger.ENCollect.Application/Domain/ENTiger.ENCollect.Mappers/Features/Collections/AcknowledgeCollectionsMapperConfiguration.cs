using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AcknowledgeCollectionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AcknowledgeCollectionsMapperConfiguration() : base()
        {
            CreateMap<AcknowledgeCollectionsDto, Collection>();
        }
    }
}