using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountsForPrimaryAllocationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAccountsForPrimaryAllocationMapperConfiguration() : base()
        {
            CreateMap<SearchAccountsForPrimaryAllocationDto, AllocationDownload>();
        }
    }
}