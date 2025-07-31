using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSecondaryAllocationDownloadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetSecondaryAllocationDownloadMapperConfiguration() : base()
        {
            CreateMap<AllocationDownload, GetSecondaryAllocationDownloadDto>();
        }
    }
}