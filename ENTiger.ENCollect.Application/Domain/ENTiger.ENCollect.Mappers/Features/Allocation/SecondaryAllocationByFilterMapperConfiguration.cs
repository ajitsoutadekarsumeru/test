using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationByFilterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SecondaryAllocationByFilterMapperConfiguration() : base()
        {
            CreateMap<SecondaryAllocationByFilterDto, LoanAccount>();
        }
    }
}