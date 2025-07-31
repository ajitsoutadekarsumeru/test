using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchPrimaryAllocationbyFiltersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchPrimaryAllocationbyFiltersMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, SearchPrimaryAllocationbyFiltersDto>();
        }
    }
}