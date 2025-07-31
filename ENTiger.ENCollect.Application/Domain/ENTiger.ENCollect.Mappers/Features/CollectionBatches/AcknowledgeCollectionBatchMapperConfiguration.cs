using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AcknowledgeCollectionBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AcknowledgeCollectionBatchMapperConfiguration() : base()
        {
            CreateMap<AcknowledgeCollectionBatchDto, CollectionBatch>();
        }
    }
}