using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryUnAllocationByBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public PrimaryUnAllocationByBatchMapperConfiguration() : base()
        {
            CreateMap<PrimaryUnAllocationByBatchDto, PrimaryUnAllocationFile>();
        }
    }
}