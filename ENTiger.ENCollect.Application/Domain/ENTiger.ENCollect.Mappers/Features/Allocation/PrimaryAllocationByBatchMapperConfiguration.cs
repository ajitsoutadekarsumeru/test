using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryAllocationByBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PrimaryAllocationByBatchMapperConfiguration() : base()
        {
            CreateMap<PrimaryAllocationByBatchDto, PrimaryAllocationFile>();
        }
    }
}