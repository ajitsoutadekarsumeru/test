using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AllocationDownloadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AllocationDownloadMapperConfiguration() : base()
        {
            CreateMap<AllocationDownloadDto, AllocationDownload>();
            CreateMap<AllocationDownload, AllocationDownloadDto>();
            CreateMap<AllocationDownloadDtoWithId, AllocationDownload>();
            CreateMap<AllocationDownload, AllocationDownloadDtoWithId>();
        }
    }
}