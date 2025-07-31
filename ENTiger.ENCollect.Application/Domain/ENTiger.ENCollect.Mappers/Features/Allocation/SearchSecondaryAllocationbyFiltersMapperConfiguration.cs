using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchSecondaryAllocationbyFiltersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchSecondaryAllocationbyFiltersMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, SearchSecondaryAllocationbyFiltersDto>();
        }
    }
}