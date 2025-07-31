using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryUnAllocationByBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SecondaryUnAllocationByBatchMapperConfiguration() : base()
        {
            CreateMap<SecondaryUnAllocationByBatchDto, SecondaryUnAllocationFile>();
        }
    }
}