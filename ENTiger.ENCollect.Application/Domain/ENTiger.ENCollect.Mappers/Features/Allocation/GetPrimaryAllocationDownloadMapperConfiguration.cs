using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPrimaryAllocationDownloadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetPrimaryAllocationDownloadMapperConfiguration() : base()
        {
            CreateMap<AllocationDownload, GetPrimaryAllocationDownloadDto>();
        }
    }
}