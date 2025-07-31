using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationFileMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SecondaryAllocationFileMapperConfiguration() : base()
        {
            CreateMap<SecondaryAllocationFileDto, SecondaryAllocationFile>();
            CreateMap<SecondaryAllocationFile, SecondaryAllocationFileDto>();
            CreateMap<SecondaryAllocationFileDtoWithId, SecondaryAllocationFile>();
            CreateMap<SecondaryAllocationFile, SecondaryAllocationFileDtoWithId>();
        }
    }
}