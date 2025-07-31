using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CollectionMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CollectionMapperConfiguration() : base()
        {
            CreateMap<CollectionDto, Collection>();
            CreateMap<Collection, CollectionDto>();
            CreateMap<CollectionDtoWithId, Collection>();
            CreateMap<Collection, CollectionDtoWithId>();
        }
    }
}