using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CollectionUploadFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CollectionUploadFileMapperConfiguration() : base()
        {
            CreateMap<CollectionUploadFileDto, CollectionUploadFile>();
            CreateMap<CollectionUploadFile, CollectionUploadFileDto>();
            CreateMap<CollectionUploadFileDtoWithId, CollectionUploadFile>();
            CreateMap<CollectionUploadFile, CollectionUploadFileDtoWithId>();

        }
    }
}
