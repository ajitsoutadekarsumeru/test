using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAccountsForSecondaryAllocationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAccountsForSecondaryAllocationMapperConfiguration() : base()
        {
            CreateMap<SearchAccountsForSecondaryAllocationDto, AllocationDownload>();
        }
    }
}