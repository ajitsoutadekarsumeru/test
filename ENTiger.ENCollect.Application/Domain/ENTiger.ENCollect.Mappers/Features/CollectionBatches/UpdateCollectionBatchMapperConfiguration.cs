using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateCollectionBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateCollectionBatchMapperConfiguration() : base()
        {
            CreateMap<UpdateCollectionBatchDto, CollectionBatch>();
        }
    }
}