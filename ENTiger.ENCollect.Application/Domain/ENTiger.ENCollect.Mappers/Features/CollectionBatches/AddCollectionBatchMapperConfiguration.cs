using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCollectionBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddCollectionBatchMapperConfiguration() : base()
        {
            CreateMap<AddCollectionBatchDto, CollectionBatch>();
        }
    }
}