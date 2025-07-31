using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CollectionBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CollectionBatchMapperConfiguration() : base()
        {
            CreateMap<CollectionBatchDto, CollectionBatch>();
            CreateMap<CollectionBatch, CollectionBatchDto>();
            CreateMap<CollectionBatchDtoWithId, CollectionBatch>();
            CreateMap<CollectionBatch, CollectionBatchDtoWithId>();
        }
    }
}