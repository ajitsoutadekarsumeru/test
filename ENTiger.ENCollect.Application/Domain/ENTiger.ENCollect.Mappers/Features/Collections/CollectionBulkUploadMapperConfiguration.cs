using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CollectionBulkUploadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CollectionBulkUploadMapperConfiguration() : base()
        {
            CreateMap<CollectionBulkUploadDto, CollectionUploadFile>();

        }
    }
}
