using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionBatchesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCollectionBatchesMapperConfiguration() : base()
        {
            CreateMap<CollectionBatch, GetCollectionBatchDto>();
        }
    }
}