using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryAllocationByFilterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PrimaryAllocationByFilterMapperConfiguration() : base()
        {
            CreateMap<PrimaryAllocationByFilterDto, LoanAccount>();
        }
    }
}