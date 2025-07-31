using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationByBatchMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SecondaryAllocationByBatchMapperConfiguration() : base()
        {
            CreateMap<SecondaryAllocationByBatchDto, SecondaryAllocationFile>();
        }
    }
}